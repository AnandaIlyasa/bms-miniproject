namespace bms.View;

using bms.Utils;
using bms.Model;

internal class SuperAdminView
{
    public void MainMenu(List<Role> _roleList, List<User> _userList)
    {
        while (true)
        {
            Console.WriteLine("\n=== Super Admin Menu ===");
            Console.WriteLine("1. Create New User");
            Console.WriteLine("2. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                CreateNewUser(_roleList, _userList);
            }
            else
            {
                Console.WriteLine("\nYou Logged Out\n");
                break;
            }
        }
    }

    void CreateNewUser(List<Role> _roleList, List<User> _userList)
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
