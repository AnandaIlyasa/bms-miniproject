using Bts.IService;
using Bts.Model;
using Bts.IRepo;
using Bts.Config;
using Bts.Constant;

namespace Bts.Service;

internal class UserService : IUserService
{
    IUserRepo _userRepo;
    IRoleRepo _roleRepo;

    public UserService(IUserRepo userRepo, IRoleRepo roleRepo)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
    }

    public User CreateUser(User user)
    {
        using (var context = new DBContextConfig())
        {
            user = _userRepo.CreateNewUser(user, context);
        }
        return user;
    }

    public List<Role> GetAllRoleExcludingSuperadminAndCandidate()
    {
        var roleList = new List<Role>();
        using (var context = new DBContextConfig())
        {
            roleList = _roleRepo.GetRoleListExcludingSuperadminAndCandidate(RoleCode.SuperAdmin, RoleCode.Candidate, context);
        }
        return roleList;
    }

    public List<User> GetCandidateList(int candidateRoleId)
    {
        var candidateList = new List<User>();
        using (var context = new DBContextConfig())
        {
            candidateList = _userRepo.GetCandidateList(candidateRoleId, context);
        }
        return candidateList;
    }

    public List<User> GetReviewerList(int reviewerRoleId)
    {
        var reviewerList = new List<User>();
        using (var context = new DBContextConfig())
        {
            reviewerList = _userRepo.GetReviewerList(reviewerRoleId, context);
        }
        return reviewerList;
    }

    public Role GetCandidateRole()
    {
        Role? candidateRole = null;
        using (var context = new DBContextConfig())
        {

            candidateRole = _roleRepo.GetCandidateRole(RoleCode.Candidate, context);
        }
        return candidateRole;
    }

    public Role RoleGetReviewerRole()
    {
        Role? reviewerRole = null;
        using (var context = new DBContextConfig())
        {
            reviewerRole = _roleRepo.GetCandidateRole(RoleCode.Reviewer, context);
        }
        return reviewerRole;
    }

    public User? Login(string email, string password)
    {
        User? user = null;
        using (var context = new DBContextConfig())
        {
            user = _userRepo.GetUserByEmailAndPassword(email, password, context);
        }
        return user;
    }
}
