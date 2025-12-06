using System.Threading.Tasks;

namespace wms_android.Interfaces
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string title, string message);
        Task<bool> ShowConfirmationAsync(string title, string message);
        Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel);
        Task<string> ShowPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel");
        
        /// <summary>
        /// Shows a brief toast notification with a message
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task ShowToastAsync(string message);
    }
} 