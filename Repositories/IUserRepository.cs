using CartManagementMVC.Models;

namespace CartManagementMVC.Repositories
{
    public interface IUserRepository
    {
        bool SaveUser(User user);
        User ValidateUser(string username, string password);
    }
}
