namespace Bts.IService;

using Bts.Model;

internal interface IUserService
{
    User? Login(string email, string password);
    User CreateUser(User user);
    List<User> GetUserListByRole(string roleCode);
    List<Role> GetAllRoleExcludingSuperadminAndCandidate();
    Role GetCandidateRole();
    List<User> GetCandidateList();
    List<User> GetReviewerList();
}
