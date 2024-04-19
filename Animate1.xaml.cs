using HelloMaui1.ViewModels;

namespace HelloMaui1;

public partial class Animate1 : ContentPage
{
	public Animate1()
	{
		InitializeComponent();

		Animate1ViewModel vm = (Animate1ViewModel)this.BindingContext;

		vm.RotateElement = async (bool front) =>
		{
			var source = front ? frontElement : backElement;
			var target = !front ? frontElement : backElement;

            //Animation
            await source.RotateYTo(front?180:-180);

            //Under the hood
            await source.ScaleTo(0, 0);//hide source
			_ = source.RotateYTo(0, 0);//reset source
            _ = target.ScaleTo(1, 0);//show target

        };
    }
}