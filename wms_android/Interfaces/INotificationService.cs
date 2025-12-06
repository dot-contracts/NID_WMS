using System.Threading.Tasks;

namespace wms_android.Interfaces
{
    public interface INotificationService
    {
        Task ShowSuccessNotificationAsync(string message, int durationSeconds = 6);
        Task ShowErrorNotificationAsync(string message, int durationSeconds = 6);
        Task ShowInfoNotificationAsync(string message, int durationSeconds = 6);
    }
}