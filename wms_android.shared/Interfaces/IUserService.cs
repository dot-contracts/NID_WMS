using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wms_android.shared.Models;

namespace wms_android.shared.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName);
        Task<User> CreateUserAsync(User user);
        Task<User> LoginAsync(Credentials credentials);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<int> GetNewClientCountForDateAsync(DateTime date);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
