using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class UserService : IUserService
{
    public User CreateUser(User user)
    {
        return new User();
    }

    public List<User> GetUserListByRole(string roleCode)
    {
        return new List<User>();
    }

    public User? Login(string email, string password)
    {
        return new User();
    }
}
