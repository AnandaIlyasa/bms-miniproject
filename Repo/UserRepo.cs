using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using Microsoft.EntityFrameworkCore;

namespace Bts.Repo;

internal class UserRepo : IUserRepo
{
    public User CreateNewUser(User user, DBContextConfig context)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }

    public List<User> GetUserListByRole(int roleId, DBContextConfig context)
    {
        var userList = context.Users
                            .Where(u => u.RoleId == roleId)
                            .ToList();
        return userList;
    }

    public User? GetUserByEmailAndPassword(string email, string password, DBContextConfig context)
    {
        User? user = null;
        user = context.Users
                .Where(u => u.Email == email && EF.Functions.Like(u.Pass, password) && u.IsActive == true)
                .Include(u => u.Role)
                .FirstOrDefault();
        return user;
    }

    public int UpdateUserIsActive(User user, DBContextConfig context)
    {
        var foundUser = context.Users
                        .Where(u => u.Id == user.Id)
                        .First();
        foundUser.IsActive = user.IsActive;

        return context.SaveChanges();
    }

    public User GetUserById(int userId, DBContextConfig context)
    {
        var user = context.Users
                    .Where(u => u.Id == userId)
                    .First();
        return user;
    }
}
