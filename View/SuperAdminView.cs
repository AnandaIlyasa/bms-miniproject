namespace Bts.View;

using Bts.Utils;
using Bts.Model;
using Bts.IService;

internal class SuperAdminView : BaseView
{
    readonly IUserService _userService;
    readonly IExamService _examService;
    User _superadminUser;

    public SuperAdminView(IUserService userService, IExamService examService)
    {
        _userService = userService;
        _examService = examService;
    }

    public void MainMenu(User user)
    {
        _superadminUser = user;
        while (true)
        {
            Console.WriteLine("\n=== Super Admin Menu ===");
            Console.WriteLine("1. Create New User");
            Console.WriteLine("2. Show Exam List");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                CreateNewUser();
            }
            else if (selectedOpt == 2)
            {
                ShowExamList(_examService);
            }
            else
            {
                Console.WriteLine("\nYou Logged Out\n");
                break;
            }
        }
    }

    void CreateNewUser()
    {
        Console.WriteLine("\nSelect Role");
        var roleList = _userService.GetAllRoleExcludingSuperadminAndCandidate();
        var number = 1;
        foreach (var role in roleList)
        {
            Console.WriteLine(number + ". " + role.RoleName);
            number++;
        }
        Console.WriteLine(number + ". Cancel");
        var selectedOpt = Utils.GetNumberInputUtil(1, number);

        if (selectedOpt == number)
        {
            Console.WriteLine("\nCreate new user cancelled");
            return;
        }

        var roleId = roleList[selectedOpt - 1].Id;
        var fullName = Utils.GetStringInputUtil("Full name");
        var email = Utils.GetStringInputUtil("Email");
        var newUser = new User()
        {
            FullName = fullName,
            Email = email,
            Pass = Utils.GenerateRandomAlphaNumericUtil(),
            RoleId = roleId,
        };

        _userService.CreateUser(newUser);

        var createdRoleName = roleList.Find(role => role.Id == roleId)!.RoleName;
        Console.WriteLine($"\nNew {createdRoleName} for {fullName} with email {email} successfully created!");
    }
}
