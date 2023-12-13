using Bts.Model;

namespace Bts.IRepo;

internal interface IUserRepo
{
    User? GetUserByEmailAndPassword(string email, string password);
    User CreateNewUser(User user);
}
