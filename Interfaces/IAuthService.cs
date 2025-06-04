
using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserRegistrationModel userdto);
        Task<string> LoginAsync(UserLoginModel userLogin);
        Task<bool> LogoutAsync();
    }
}