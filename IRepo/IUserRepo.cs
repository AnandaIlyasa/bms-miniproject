using Bts.Config;
using Bts.Constant;
using Bts.Model;

namespace Bts.IRepo;

internal interface IUserRepo
{
    User? GetUserByEmailAndPassword(string email, string password, DBContextConfig context);
    User CreateNewUser(User user, DBContextConfig context);
    List<User> GetCandidateList(int candidateRoleId, DBContextConfig context);
    List<User> GetReviewerList(int reviewerRoleId, DBContextConfig context);
}
