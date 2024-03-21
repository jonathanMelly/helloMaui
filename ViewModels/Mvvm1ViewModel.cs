using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace HelloMaui1.ViewModels
{
    public partial class Mvvm1ViewModel : ObservableObject
    {
        [ObservableProperty]
        private int counter = 0;

        [RelayCommand]
        private void Increment(int incrementValue)
        {
            Counter += incrementValue;
        }
    }
}

