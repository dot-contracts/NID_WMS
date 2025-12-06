using wms_android.shared.Models.Auth;

namespace wms_android.shared.Interfaces
{
    public interface IAuthService
    {
        // Task<string> GenerateJwtTokenAsync(string username, string password);
        Task<bool> ValidateTokenAsync(string token);
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }
} 