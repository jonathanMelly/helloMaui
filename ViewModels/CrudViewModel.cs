using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace HelloMaui1.ViewModels
{
    public sealed partial class CrudViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> wishes = new() { "décrocher la lune", "voler dans les airs" };

        [RelayCommand]
        private void AddWish(string wish)
        {
            Wishes.Add(wish);
        }

    }
}
