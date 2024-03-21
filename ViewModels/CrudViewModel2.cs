using Android.Content;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloMaui1.Models;
using HelloMaui1.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HelloMaui1.ViewModels
{
    public sealed partial class CrudViewModel2 : ObservableObject
    {

        public CrudViewModel2()
        {
            RefreshWishesFromDB();
        }

        private void RefreshWishesFromDB(AladdinContext? context=null)
        {
            Wishes.Clear();
            using (var dbContext = context??new AladdinContext())
            {
                foreach (var dbWish in dbContext.Wishes)
                {
                    Wishes.Add(dbWish);
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<Wish> wishes = new();

        [ObservableProperty]
        private Wish? selectedWish;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddWishCommand))]
        private string wishEntry = "";

        [RelayCommand(CanExecute = nameof(AddWishCanExecute))]
        private async Task AddWish(string definition)
        {
            var wish = new Wish { Definition = definition };
            using (var dbContext = new AladdinContext())
            {
                dbContext.Add(wish);
                await dbContext.SaveChangesAsync();
            }
            //Avoid full db refresh upon adding a new wish
            Wishes.Add(wish);
            WishEntry = "";
        }

        private bool AddWishCanExecute()
        {
            return !string.IsNullOrEmpty(WishEntry);
        }

        [RelayCommand]
        private async Task Edit(Wish wish)
        {
            Trace.WriteLine($"Editing {wish}");

            //Affiche un popup pour demander la modification
            ///!\ Court-circuite MVVM mais toléré pour ne pas ajouter plus de complexité pour l’instant/!\
            string updatedDefinition = await Shell.Current.DisplayPromptAsync(title: "Modifier ", message: "", placeholder: wish.Definition);

            //Si l’utilisateur n’appuie pas sur Cancel
            if(updatedDefinition!=null)
            {
                using (var dbContext = new AladdinContext())
                {
                    //TODO : Faire la mise à jour uniquement si la définition a changé

                    await dbContext.Wishes
                        .Where(dbWish => dbWish.Id==wish.Id)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(dbWish => dbWish.Definition, updatedDefinition));

                    /* Version "old style" moins optimale laissée à des fins pédagogiques
                    var dbWish = dbContext.Wishes.Single(dbWish => dbWish.Id== wish.Id);
                    dbWish.Definition = updatedDefinition;
                    await dbContext.SaveChangesAsync();
                    */

                    //Et on rafraîchit la liste locale
                    RefreshWishesFromDB(dbContext);
                }
            }
        }


        [RelayCommand]
        private async Task Delete(Wish wish)
        {
            Trace.WriteLine($"Deleting {wish}");
            using (var dbContext = new AladdinContext())
            {
                await dbContext.Wishes
                        .Where(dbWish => dbWish.Id == wish.Id)
                        .ExecuteDeleteAsync();

                //Refresh local list reusing current dbcontext
                RefreshWishesFromDB(dbContext);
            }
            
        }

    }
}
