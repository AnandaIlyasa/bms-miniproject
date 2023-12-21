using Bts.IService;
using Bts.Model;
using Bts.IRepo;
using Bts.Config;
using Bts.Constant;
using Bts.Helper;
using Bts.Utils;

namespace Bts.Service;

internal class UserService : IUserService
{
    IUserRepo _userRepo;
    IRoleRepo _roleRepo;
    SessionHelper _sessionHelper;

    public UserService(IUserRepo userRepo, IRoleRepo roleRepo, SessionHelper sessionHelper)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
        _sessionHelper = sessionHelper;
    }

    public User CreateUser(User user, string roleCode)
    {
        user.Pass = Utils.Utils.GenerateRandomAlphaNumericUtil();
        using (var context = new DBContextConfig())
        {
            var role = _roleRepo.GetRoleByCode(roleCode, context);

            user.RoleId = role.Id;
            user.CreatedBy = _sessionHelper.UserId;
            user = _userRepo.CreateNewUser(user, context);
        }
        return user;
    }

    public List<Role> GetReviewerAndHRRole()
    {
        var roleList = new List<Role>();
        using (var context = new DBContextConfig())
        {
            roleList = _roleRepo.GetRoleList(context);
        }
        roleList = roleList
                .Where(r => r.RoleCode == UserRoleCode.Reviewer || r.RoleCode == UserRoleCode.HumanResource)
                .ToList();
        return roleList;
    }

    public List<User> GetCandidateList()
    {
        var candidateList = new List<User>();
        using (var context = new DBContextConfig())
        {
            var candidateRole = _roleRepo.GetRoleByCode(UserRoleCode.Candidate, context);
            candidateList = _userRepo.GetUserListByRole(candidateRole.Id, context);
        }
        return candidateList;
    }

    public List<User> GetReviewerList()
    {
        var reviewerList = new List<User>();
        using (var context = new DBContextConfig())
        {
            var reviewerRole = _roleRepo.GetRoleByCode(UserRoleCode.Reviewer, context);
            reviewerList = _userRepo.GetUserListByRole(reviewerRole.Id, context);
        }
        return reviewerList;
    }

    public User? Login(string email, string password)
    {
        User? user = null;
        using (var context = new DBContextConfig())
        {
            user = _userRepo.GetUserByEmailAndPassword(email, password, context);
        }
        if (user != null)
        {
            _sessionHelper.UserId = user.Id;
        }
        return user;
    }
}
