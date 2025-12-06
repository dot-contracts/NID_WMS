using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wms_android.shared.Models;

namespace wms_android.data.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<int> GetNewClientCountForDateAsync(DateTime date);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        
        // New method for dashboard
        Task<int> GetNewClientCountAsync();
    }
}
