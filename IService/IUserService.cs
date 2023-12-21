namespace Bts.IService;

using Bts.Model;

internal interface IUserService
{
    User? Login(string email, string password);
    User CreateUser(User user, string roleCode);
    List<Role> GetReviewerAndHRRole();
    List<User> GetCandidateList();
    List<User> GetReviewerList();
}
