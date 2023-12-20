namespace Bts.IService;

using Bts.Model;

internal interface IUserService
{
    User? Login(string email, string password);
    User CreateUser(User user);
    List<Role> GetAllRoleExcludingSuperadminAndCandidate();
    Role RoleGetReviewerRole();
    Role GetCandidateRole();
    List<User> GetCandidateList(int candidateRoleId);
    List<User> GetReviewerList(int reviewerRoleId);
}
