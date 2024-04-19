using HelloMaui1.ViewModels;

namespace HelloMaui1;

public partial class Animate2 : ContentPage
{
	public Animate2()
	{
		InitializeComponent();

		var vm = BindingContext as Animate2ViewModel;

        //Écriture standard avec une méthode liée
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
        vm.RotateBoxUIAction = RotateUI;
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.

        #region move
        //Écriture raccourcie avec un lambda
        vm.MoveBoxUIAction = (int x) =>
        {
            this.box.TranslationX += x;
        };
        #endregion
    }

    private void RotateUI(int angle)
	{
        this.box.RotateTo(angle);
    }
}