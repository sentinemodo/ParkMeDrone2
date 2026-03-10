using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DJI.WindowsSDK;

namespace ParkMeDrone2.Pages
{
    public sealed partial class ActivatingPage : Page
    {
        public ActivatingPage()
        {
            InitializeComponent();
            DJISDKManager.Instance.SDKRegistrationStateChanged += OnSdkRegistrationStateChanged;
        }

        private async void OnSdkRegistrationStateChanged(SDKRegistrationState state, SDKError resultCode)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ActivateStateText.Text = state == SDKRegistrationState.Succeeded ? "Activated." : "Not activated.";
                ActivationInfo.Text = resultCode == SDKError.NO_ERROR ? "Registration succeeded." : resultCode.ToString();
            });
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var appKey = AppKeyBox?.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(appKey))
            {
                ActivationInfo.Text = "Enter your App Key first.";
                return;
            }
            DJISDKManager.Instance.RegisterApp(appKey);
            ActivationInfo.Text = "Registering...";
        }
    }
}
