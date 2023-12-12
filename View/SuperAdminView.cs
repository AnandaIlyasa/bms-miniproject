namespace Bts.View;

using Bts.Utils;
using Bts.Model;

internal class SuperAdminView : BaseView
{
    public void MainMenu(List<Role> _roleList)
    {
        while (true)
        {
            Console.WriteLine("\n=== Super Admin Menu ===");
            Console.WriteLine("1. Create New User");
            Console.WriteLine("2. Show Exam List");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                CreateNewUser(_roleList);
            }
            else if (selectedOpt == 2)
            {
                ShowExamList();
            }
            else
            {
                Console.WriteLine("\nYou Logged Out\n");
                break;
            }
        }
    }

    void CreateNewUser(List<Role> _roleList)
    {
        Console.WriteLine("\nSelect Role");
        Console.WriteLine("1. Human Resource");
        Console.WriteLine("2. Reviewer");
        var selectedOpt = Utils.GetNumberInputUtil(1, 2);

        Role role;
        if (selectedOpt == 1)
        {
            role = _roleList[1];
        }
        else
        {
            role = _roleList[2];
        }

        var fullName = Utils.GetStringInputUtil("Full name");
        var email = Utils.GetStringInputUtil("Email");

        Console.WriteLine($"\nNew {role.RoleName} for {fullName} with email {email} already created!");
    }
}
