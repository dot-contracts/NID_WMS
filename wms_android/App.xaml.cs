using Microsoft.Maui.Controls;
using wms_android.Views;
using wms_android.ViewModels;
using Java.Util;
using wms_android.shared.Interfaces;
using Microsoft.Maui.Storage;
using wms_android.Interfaces;

namespace wms_android
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private ISessionTimeoutService _sessionTimeout;
        private Services.UpdateManager _updateManager;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            // Create and configure the AppShell
            var appShell = new AppShell();
            
            // Set the MainPage to the AppShell with the SplashScreen
            MainPage = appShell;
            
            // Register the SplashScreen
            Routing.RegisterRoute(nameof(SplashScreen), typeof(SplashScreen));
            
            // For testing: Add mock authentication data
            Preferences.Set("AuthToken", "test-token-for-development");
            Preferences.Set("CurrentUsername", "test-user");
            Preferences.Set("UserRole", "Clerk");
            Preferences.Set("CurrentUserId", 1);

            // Initialize session timeout service
            InitializeSessionTimeout();
            
            // Initialize update manager
            InitializeUpdateManager();

            // Navigate directly to the splash screen first
            Microsoft.Maui.Controls.Shell.Current.GoToAsync("//SplashScreen");

            // Original login code (commented for testing)
            // Microsoft.Maui.Controls.Shell.Current.GoToAsync("//LoginPage");

            // Commented out previous direct navigation code
            // var parcelsViewModel = serviceProvider.GetRequiredService<ParcelsViewModel>();
            // MainPage = new NavigationPage(new ParcelsView(parcelsViewModel));

            // Set the LoginPage without passing navigation initially
            //var userService = serviceProvider.GetRequiredService<IUserService>();
            //var navigation = new NavigationPage();
            //var loginViewModel = new LoginViewModel(userService, navigation.Navigation);
            //var loginPage = new LoginPage(loginViewModel);

            //navigation.PushAsync(loginPage);
            //MainPage = navigation;  


            //var vehicleService = serviceProvider.GetRequiredService<IVehicleService>();
            //MainPage = new NavigationPage(new VehicleSelectionPage(vehicleService));

            //var confirmparcelsViewModel = serviceProvider.GetRequiredService<ConfirmParcelsViewModel>();
            //MainPage = new NavigationPage(new ConfirmParcelsView(confirmparcelsViewModel));
        }

        private void InitializeSessionTimeout()
        {
            _sessionTimeout = _serviceProvider.GetService<ISessionTimeoutService>();
            if (_sessionTimeout != null)
            {
                _sessionTimeout.SessionExpired += OnSessionExpired;
            }
        }
        
        private void InitializeUpdateManager()
        {
            _updateManager = _serviceProvider.GetService<Services.UpdateManager>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            // Start session timeout if user is logged in
            if (IsUserLoggedIn())
            {
                _sessionTimeout?.StartSession();
            }
            
            // Start update monitoring
            _updateManager?.StartMonitoringAsync();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            // Stop session timeout when app goes to background
            _sessionTimeout?.StopSession();
            // Stop update monitoring when app goes to background
            _updateManager?.StopMonitoringAsync();
        }

        protected override void OnResume()
        {
            base.OnResume();
            
            // Resume session timeout if user is logged in
            if (IsUserLoggedIn())
            {
                _sessionTimeout?.StartSession();
            }
            
            // Resume update monitoring
            _updateManager?.StartMonitoringAsync();
        }

        private bool IsUserLoggedIn()
        {
            return !string.IsNullOrEmpty(Preferences.Get("AuthToken", string.Empty));
        }

        private async void OnSessionExpired(object sender, EventArgs e)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    // Show session expired message
                    await DisplayAlert("Session Expired", "Your session has expired due to inactivity. Please log in again.", "OK");
                    
                    // Clear stored credentials
                    ClearUserSession();
                    
                    // Navigate to login page
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error handling session expiration: {ex.Message}");
                }
            });
        }

        private void ClearUserSession()
        {
            Preferences.Remove("AuthToken");
            Preferences.Remove("CurrentUsername");
            Preferences.Remove("UserRole");
            Preferences.Remove("CurrentUserId");
        }

        private async Task<bool> DisplayAlert(string title, string message, string accept)
        {
            if (MainPage != null)
            {
                await MainPage.DisplayAlert(title, message, accept);
                return true;
            }
            return false;
        }
    }
}
