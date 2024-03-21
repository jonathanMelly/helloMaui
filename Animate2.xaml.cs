using HelloMaui1.ViewModels;

namespace HelloMaui1;

public partial class Animate2 : ContentPage
{
	public Animate2()
	{
		InitializeComponent();

		var vm = BindingContext as Animate2ViewModel;
		vm.RotateBoxUIAction = (int angle) =>
		{
			this.box.RotateTo(angle);
		};
	}
}