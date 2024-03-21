using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMaui1.ViewModels
{
    internal partial class SensorViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? sensorValue;

        [ObservableProperty]
        private bool cancel;

        [ObservableProperty]
        private string cancelColor="red";

        [ObservableProperty]
        private float threshold;

        [ObservableProperty]
        private double speed;

        [ObservableProperty]
        private bool toggled;

        //thanks to https://blog.ewers-peters.de/mvvm-source-generators-advanced-scenarios
        partial void OnToggledChanged(bool  value)
        {
            ToggleShake();
            
        }



        Stopwatch stopwatch=new();

        [ObservableProperty]
        private string shakeColor = "black";

        [RelayCommand]
        private void Enable()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    // Turn on accelerometer
                    Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;
                    Accelerometer.Default.Start(SensorSpeed.UI);
                }
                else
                {
                    // Turn off accelerometer
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;
                }
            }
        }

        private void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
        {
            SensorValue = e.Reading.ToString();
            var data = e.Reading.Acceleration.Y;
            Speed = Math.Sqrt(data * data);
            var thresholdPassed = Speed  >= Threshold;

            if(thresholdPassed)
            {
                if(!Cancel)
                {
                    stopwatch.Reset();
                    stopwatch.Start();
                    Cancel = true;
                }
                else
                {
                    if (stopwatch.ElapsedMilliseconds > 3000)
                    {
                        CancelColor = "blue";
                        stopwatch.Stop();
                    }
                }
            }
            else
            {
                Cancel = false;
                stopwatch.Stop();
            }
        }


        //To simulate : adb emu sensor set acceleration 0:50:0 && timeout 1 && adb emu sensor set acceleration 0:0:0
        [RelayCommand]
        private void ToggleShake()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    // Turn on accelerometer
                    Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                    Accelerometer.Default.Start(SensorSpeed.Default);
                }
                else
                {
                    // Turn off accelerometer
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
                    ShakeColor = "black";

                }
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            // Update UI Label with a "shaked detected" notice, in a randomized color
            //ShakeLabel.TextColor = new Color(Random.Shared.Next(256), Random.Shared.Next(256), Random.Shared.Next(256));
            //ShakeLabel.Text = $"Shake detected";
            ShakeColor = "red";


        }

        private async void ConcurrentAwareMethod0()
        {

        }

        private async void ConcurrentAwareMethod()
        {
            Shell.Current.Navigation.PushAsync(new Details());
        }

        private async void ConcurrentAwareMethod1()
        {
            Shell.Current.Navigation.PushAsync(new Details());

            Trace.WriteLine("Ligne éxécutée SANS attendre le changement de page");
        }

        private async void ConcurrentAwareMethod1bis()
        {
            await Shell.Current.Navigation.PushAsync(new Details());

            Trace.WriteLine("Ligne éxécutée APRÈS le changement de page");
        }
    }
}
