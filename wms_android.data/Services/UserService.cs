using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using wms_android.shared.Data;
using wms_android.shared.Models;
using wms_android.data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace wms_android.data.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public UserService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            
            // Debug: Print all users in the database
            var allUsers = await context.Users.ToListAsync();
            Console.WriteLine($"Total users in database: {allUsers.Count}");
            foreach (var u in allUsers)
            {
                Console.WriteLine($"User: {u.Username}, Role: {u.RoleId}");
            }

            // Try to find the user without using FirstOrDefaultAsync
            var users = await context.Users
                .Include(u => u.Role)
                .Where(u => u.Username == username)
                .ToListAsync();

            Console.WriteLine($"Users found with username '{username}': {users.Count}");

            if (users.Count > 0)
            {
                var user = users[0];
                Console.WriteLine($"Found user: {user.Username}, Role: {user.RoleId}");

                if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return user;
                }
                else
                {
                    Console.WriteLine("Password verification failed");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"User not found: {username}");
                return null;
            }
        }

        public async Task<int> GetNewClientCountForDateAsync(DateTime date)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Users
                .CountAsync(u => u.CreatedAt.Date == date.Date && u.Role.Name == "Client");
        }
        
        /// <summary>
        /// Gets the total count of new clients
        /// </summary>
        /// <returns>Count of new clients today</returns>
        public async Task<int> GetNewClientCountAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                // Count clients created in the last 24 hours
                var today = DateTime.UtcNow.Date;
                return await context.Users
                    .CountAsync(u => u.CreatedAt.Date == today && u.Role.Name == "Client");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting new client count: {ex.Message}");
                return 0;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.CreatedAt = DateTime.UtcNow;

            using var context = await _contextFactory.CreateDbContextAsync();
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            using var context = await _contextFactory.CreateDbContextAsync();
            var existingUser = await context.Users.FindAsync(user.Id);
            if (existingUser == null)
                throw new InvalidOperationException("User not found");

            // Update user properties
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.RoleId = user.RoleId;
            // Add other properties as needed

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
