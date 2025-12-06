using wms_android.Interfaces;
using wms_android.Controls;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace wms_android.Services
{
    public class NotificationService : INotificationService
    {
        public async Task ShowSuccessNotificationAsync(string message, int durationSeconds = 6)
        {
            await ShowNotificationAsync("Success!", message, durationSeconds);
        }

        public async Task ShowErrorNotificationAsync(string message, int durationSeconds = 6)
        {
            await ShowNotificationAsync("Error", message, durationSeconds);
        }

        public async Task ShowInfoNotificationAsync(string message, int durationSeconds = 6)
        {
            await ShowNotificationAsync("Information", message, durationSeconds);
        }

        private async Task ShowNotificationAsync(string title, string message, int durationSeconds)
        {
            // Wait a bit to ensure navigation is complete
            await Task.Delay(100);
            
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    var mainPage = Application.Current?.MainPage;
                    if (mainPage == null)
                        return;

                    var notificationOverlay = new NotificationOverlay();

                    // Get the current displayed page (could be Shell content)
                    ContentPage targetPage = null;
                    if (mainPage is Shell shell && shell.CurrentPage is ContentPage contentPage)
                    {
                        targetPage = contentPage;
                    }
                    else if (mainPage is ContentPage directPage)
                    {
                        targetPage = directPage;
                    }

                    if (targetPage?.Content is Grid grid)
                    {
                        // Add overlay to existing grid
                        grid.Children.Add(notificationOverlay);
                        
                        // Show the notification
                        await notificationOverlay.ShowAsync(title, message, durationSeconds);
                        
                        // Remove overlay
                        grid.Children.Remove(notificationOverlay);
                    }
                    else if (targetPage != null)
                    {
                        // Wrap existing content in a grid to add overlay
                        var existingContent = targetPage.Content;
                        var wrapperGrid = new Grid();
                        wrapperGrid.Children.Add(existingContent);
                        wrapperGrid.Children.Add(notificationOverlay);
                        targetPage.Content = wrapperGrid;
                        
                        // Show the notification
                        await notificationOverlay.ShowAsync(title, message, durationSeconds);
                        
                        // Restore original content
                        targetPage.Content = existingContent;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error showing notification: {ex.Message}");
                    // Fallback to simple alert
                    await Application.Current.MainPage?.DisplayAlert(title, message, "OK");
                }
            });
        }
    }
}