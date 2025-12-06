using System.Threading.Tasks;

namespace wms_android.api.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(string username, string password);
    }
} 