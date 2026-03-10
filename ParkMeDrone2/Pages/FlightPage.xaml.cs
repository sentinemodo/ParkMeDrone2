using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using DJI.WindowsSDK;
using ParkMeDrone2.Models;

namespace ParkMeDrone2.Pages
{
    public sealed partial class FlightPage : Page
    {
        public ObservableCollection<ParkingSlotMarker> Markers { get; } = new ObservableCollection<ParkingSlotMarker>();

        public FlightPage()
        {
            InitializeComponent();
            MarkersList.ItemsSource = Markers;
            Loaded += FlightPage_Loaded;
            Unloaded += FlightPage_Unloaded;
        }

        private void FlightPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateConnectionState();
            if (DJISDKManager.Instance.SDKRegistrationResultCode == SDKError.NO_ERROR)
            {
                var fc = DJISDKManager.Instance.ComponentManager.GetFlightControllerHandler(0, 0);
                if (fc != null)
                {
                    fc.AltitudeChanged += OnAltitudeChanged;
                    fc.ConnectionChanged += OnConnectionChanged;
                }
                var battery = DJISDKManager.Instance.ComponentManager.GetBatteryHandler(0, 0);
                if (battery != null) battery.ChargeRemainingInPercentChanged += OnBatteryChanged;
            }
        }

        private void FlightPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (DJISDKManager.Instance.SDKRegistrationResultCode == SDKError.NO_ERROR)
            {
                var fc = DJISDKManager.Instance.ComponentManager.GetFlightControllerHandler(0, 0);
                if (fc != null)
                {
                    fc.AltitudeChanged -= OnAltitudeChanged;
                    fc.ConnectionChanged -= OnConnectionChanged;
                }
                var battery = DJISDKManager.Instance.ComponentManager.GetBatteryHandler(0, 0);
                if (battery != null) battery.ChargeRemainingInPercentChanged -= OnBatteryChanged;
            }
        }

        private async void OnAltitudeChanged(object sender, DoubleMsg? value)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                AltitudeText.Text = value.HasValue ? $"Alt: {value.Value.value:F1} m" : "Alt: -- m";
            });
        }

        private async void OnConnectionChanged(object sender, BoolMsg? value)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, UpdateConnectionState);
        }

        private async void OnBatteryChanged(object sender, IntMsg? value)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                BatteryText.Text = value.HasValue ? $"Battery: {value.Value.value}%" : "Battery: --%";
            });
        }

        private void UpdateConnectionState()
        {
            if (DJISDKManager.Instance.SDKRegistrationResultCode != SDKError.NO_ERROR)
            {
                ConnectionText.Text = "SDK not activated";
                return;
            }
            var product = DJISDKManager.Instance?.ComponentManager;
            ConnectionText.Text = (product != null) ? "Connected" : "Disconnected";
            GpsText.Text = "GPS: --";
        }

        private void VideoArea_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var marker = new ParkingSlotMarker
            {
                RelativeX = 0.5,
                RelativeY = 0.5,
                Label = $"Slot #{Markers.Count + 1}"
            };
            Markers.Add(marker);
        }

        private async void TakeOffButton_Click(object sender, RoutedEventArgs e)
        {
            if (DJISDKManager.Instance.SDKRegistrationResultCode != SDKError.NO_ERROR)
            {
                return;
            }
            var fc = DJISDKManager.Instance.ComponentManager.GetFlightControllerHandler(0, 0);
            if (fc == null) return;
            try
            {
                await fc.StartTakeoffAsync();
            }
            catch (Exception)
            {
                // Show error if needed
            }
        }

        private async void LandButton_Click(object sender, RoutedEventArgs e)
        {
            if (DJISDKManager.Instance.SDKRegistrationResultCode != SDKError.NO_ERROR) return;
            var fc = DJISDKManager.Instance.ComponentManager.GetFlightControllerHandler(0, 0);
            if (fc == null) return;
            try
            {
                await fc.StartAutoLandingAsync();
            }
            catch (Exception) { }
        }

        private async void RthButton_Click(object sender, RoutedEventArgs e)
        {
            if (DJISDKManager.Instance.SDKRegistrationResultCode != SDKError.NO_ERROR) return;
            var fc = DJISDKManager.Instance.ComponentManager.GetFlightControllerHandler(0, 0);
            if (fc == null) return;
            try
            {
                await fc.StartGoHomeAsync();
            }
            catch (Exception) { }
        }
    }
}
