using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMaui1.ViewModels
{
    internal partial class Animate1ViewModel : ObservableObject
    {
        public Func<bool,Task>? RotateElement { get; set; }

        private bool frontSide = true;

        [ObservableProperty]
        private string text = "FRONT";

        [RelayCommand]
        private async Task Animate()
        {
            Text = "";
            await RotateElement?.Invoke(frontSide);
            //RotateElement?.Invoke(frontSide);
            frontSide = !frontSide;

            Text = frontSide?"FRONT":"BACK";
        }
    }
}
