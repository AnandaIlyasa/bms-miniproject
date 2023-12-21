using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface IUserRepo
{
    User GetUserById(int userId, DBContextConfig context);
    User? GetUserByEmailAndPassword(string email, string password, DBContextConfig context);
    User CreateNewUser(User user, DBContextConfig context);
    List<User> GetUserListByRole(int roleId, DBContextConfig context);
    int UpdateUserIsActive(User user, DBContextConfig context);
}
