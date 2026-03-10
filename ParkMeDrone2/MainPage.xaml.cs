using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DJI.WindowsSDK;
using ParkMeDrone2.Pages;

namespace ParkMeDrone2
{
    public sealed partial class MainPage : Page
    {
        private readonly List<KeyValuePair<string, Type>> _menuItems = new List<KeyValuePair<string, Type>>
        {
            new KeyValuePair<string, Type>("Activate SDK", typeof(ActivatingPage)),
            new KeyValuePair<string, Type>("Parking Scan", typeof(FlightPage)),
        };

        public MainPage()
        {
            InitializeComponent();
            foreach (var item in _menuItems)
            {
                NavView.MenuItems.Add(new Microsoft.UI.Xaml.Controls.NavigationViewItem { Content = item.Key, Tag = item.Value });
            }
            NavView.Loaded += NavView_Loaded;
            NavView.ItemInvoked += NavView_ItemInvoked;
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // Don't access the event value directly (not allowed). Check Instance instead.
            if (DJISDKManager.Instance != null)
            {
                // SDK instance exists; ensure first page is shown if needed
            }
            ContentFrame.Navigate(typeof(ActivatingPage));
        }

        private void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args?.InvokedItemContainer?.Tag is Type pageType)
            {
                ContentFrame.Navigate(pageType);
            }
        }
    }
}
