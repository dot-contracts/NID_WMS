using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using wms_android.api.Interfaces;
using wms_android.shared.Interfaces; // For IUserService
using wms_android.shared.Models.Auth; // For JwtConfig

namespace wms_android.api.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IUserService _userService; // API's user service

        public TokenService(IOptions<JwtConfig> jwtConfig, IUserService userService)
        {
            // Ensure JwtConfig is loaded
            _jwtConfig = jwtConfig?.Value ?? throw new ArgumentNullException(nameof(jwtConfig), "JwtConfig cannot be null. Ensure it is configured.");
            if (string.IsNullOrEmpty(_jwtConfig.Secret))
            {
                 throw new ArgumentNullException(nameof(_jwtConfig.Secret), "JwtConfig:Secret cannot be empty. Ensure it is configured.");
            }
            _userService = userService;
        }

        public async Task<string> GenerateJwtTokenAsync(string username, string password)
        {
            // Validate credentials first using the API's UserService
            var user = await _userService.AuthenticateAsync(username, password);
            if (user == null)
            {
                // Handle invalid credentials appropriately - maybe throw?
                // Returning null or empty might be ambiguous.
                // For now, throwing an exception as the AuthController handles Unauthorized.
                throw new UnauthorizedAccessException("Invalid credentials provided for token generation.");
            }

            // Create claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Include User ID
                new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
                // Add other claims as needed
            };

            // Get secret key
            var secretBytes = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
            var key = new SymmetricSecurityKey(secretBytes);

            // Create signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_jwtConfig.ExpiryInDays),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = credentials
            };

            // Create and write the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
} 