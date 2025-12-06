using System.Threading.Tasks;
using wms_android.Interfaces;

namespace wms_android.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowAlertAsync(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public async Task<bool> ShowConfirmationAsync(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "Yes", "No");
        }

        public async Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<string> ShowPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel")
        {
            return await Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel);
        }

        public async Task ShowToastAsync(string message)
        {
            // Use community toolkit or other toast component if available
            await Application.Current.MainPage.DisplayAlert("Information", message, "OK");
        }
    }
} 