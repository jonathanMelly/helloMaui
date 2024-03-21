using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMaui1.ViewModels
{
    partial class Animate2ViewModel:ObservableObject
    {
        [ObservableProperty]
        private bool toggled = false;

        public Action<int>? RotateBoxUIAction { set; private get; }

        private void RotateBox(int angle)
        {
            if(RotateBoxUIAction!=null)
            {
                RotateBoxUIAction.Invoke(angle);
            }
            else
            {
                Trace.WriteLine($"No rotation action defined, would rotate {angle}");
            }
           
        }

        partial void OnToggledChanged(bool value)
        {
            RotateBox(value ? 90 : 0);
        }
    }
}
