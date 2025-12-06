using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using wms_android.shared.Interfaces;
using wms_android.shared.Models.Auth;

namespace wms_android.shared.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            System.Diagnostics.Debug.WriteLine("WARN: Client-side ValidateTokenAsync called, but is likely ineffective without secret.");
            await Task.CompletedTask;
            return false;
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _userService.AuthenticateAsync(username, password);
            return user != null;
        }
    }
}