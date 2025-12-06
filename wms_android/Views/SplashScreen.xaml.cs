using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;

namespace wms_android.Views
{
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            
            // Start a timer to navigate after a delay
            Device.StartTimer(TimeSpan.FromSeconds(3), () => {
                MainThread.BeginInvokeOnMainThread(async () => {
                    // Navigate to the main dashboard page
                    await Shell.Current.GoToAsync("//ClerkDashboardView");
                });
                return false; // Don't repeat the timer
            });
        }
    }
} 