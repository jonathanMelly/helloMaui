using HelloMaui1.ViewModels;

namespace HelloMaui1;

public partial class Mvvm1 : ContentPage
{
    public Mvvm1()
    {
        InitializeComponent();
        this.BindingContext = new Mvvm1ViewModel();
    }
}