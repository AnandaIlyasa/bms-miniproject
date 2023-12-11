using bms.Model;
using System.Runtime.ConstrainedExecution;

namespace bms.View;

internal class AuthView
{
    CandidateView _candidateView;
    HRView _hrView;
    ReviewerView _reviewerView;
    SuperAdminView _superAdminView;

    static readonly List<Role> _roleList = new List<Role>()
    {
        new Role()
        {
            Id = 1,
            CreatedBy = 1,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true,
            RoleCode = "SA",
            RoleName = "Super Admin"
        },
        new Role()
        {
            Id = 2,
            CreatedBy = 1,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true,
            RoleCode = "HR",
            RoleName = "Human Resource"
        },
        new Role()
        {
            Id = 3,
            CreatedBy = 1,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true,
            RoleCode = "REV",
            RoleName = "Reviewer"
        },
        new Role()
        {
            Id = 4,
            CreatedBy = 1,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true,
            RoleCode = "CNDT",
            RoleName = "Candidate"
        }
    };

    List<User> _userList = new List<User>()
    {
        new User()
        {
            Id = 1,
            CreatedBy = 1,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true,
            FullName = "Super Admin",
            Email = "sa@gmail.com",
            Pass = "sa",
            Role = AuthView._roleList[0]
        },
        new User()
        {
            Id = 1,
            CreatedBy = 1,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true,
            FullName = "Human Resource",
            Email = "hr@gmail.com",
            Pass = "hr",
            Role = AuthView._roleList[1]
        },
        new User()
        {
            Id = 1,
            CreatedBy = 1,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true,
            FullName = "Reviewer",
            Email = "rev@gmail.com",
            Pass = "rev",
            Role = AuthView._roleList[2]
        },
        new User()
        {
            Id = 1,
            CreatedBy = 1,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true,
            FullName = "Candidate",
            Email = "can@gmail.com",
            Pass = "can",
            Role = AuthView._roleList[3]
        }
    };

    public AuthView(SuperAdminView superadminView, CandidateView candidateView, HRView hrView, ReviewerView reviewerView)
    {
        _superAdminView = superadminView;
        _candidateView = candidateView;
        _hrView = hrView;
        _reviewerView = reviewerView;
    }

    public void Login()
    {
        while (true)
        {
            Console.WriteLine("\n==== Bootcamp Test Management System ====");
            Console.Write("Email : ");
            var email = Console.ReadLine();
            Console.Write("Password : ");
            var password = Console.ReadLine();

            var (loginSuccess, role) = CheckCredential(email, password);
            while (loginSuccess == false)
            {
                Console.WriteLine("\nCredential is wrong!\n");

                Console.Write("Email : ");
                email = Console.ReadLine();
                Console.Write("Password : ");
                password = Console.ReadLine();
                (loginSuccess, role) = CheckCredential(email, password);
            }

            if (role.RoleName == _roleList[0].RoleName)
            {
                _superAdminView.MainMenu(_roleList, _userList);
            }
            else if (role.RoleName == _roleList[1].RoleName)
            {
                _hrView.MainMenu(_roleList[3], _userList);
            }
            else if (role.RoleName == _roleList[2].RoleName)
            {
                _reviewerView.MainMenu();
            }
            else if (role.RoleName == _roleList[3].RoleName)
            {
                _candidateView.MainMenu();
            }
        }
    }

    (bool, Role?) CheckCredential(string email, string password)
    {
        foreach (var user in _userList)
        {
            if (user.Email == email && user.Pass == password)
            {
                return (true, user.Role);
            }
        }

        return (false, null);
    }
}
