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

    public List<User> GetCandidateList(int candidateRoleId, DBContextConfig context)
    {
        var candidateList = context.Users
                            .Where(u => u.RoleId == candidateRoleId)
                            .ToList();
        return candidateList;
    }

    public List<User> GetReviewerList(int reviewerRoleId, DBContextConfig context)
    {
        var candidateList = context.Users
                            .Where(u => u.RoleId == reviewerRoleId)
                            .ToList();
        return candidateList;
    }

    public User? GetUserByEmailAndPassword(string email, string password, DBContextConfig context)
    {
        User? user = null;
        user = context.Users
                .Where(u => u.Email == email && EF.Functions.Like(u.Pass, password))
                .Include(u => u.Role)
                .FirstOrDefault();
        return user;
    }
}
