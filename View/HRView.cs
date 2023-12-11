namespace bms.View;

using bms.Model;
using bms.Utils;
using System.Data;

internal class HRView
{
    public void MainMenu(Role candidateRole, List<User> _userList)
    {
        while (true)
        {
            Console.WriteLine("\n=== Human Resource Menu ===");
            Console.WriteLine("1. Create New Candidate");
            Console.WriteLine("2. Create New Package");
            // 3. assign exam to candidate and reviewer
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                CreateNewCandidate(candidateRole, _userList);
            }
            else if (selectedOpt == 2)
            {
                CreateNewPackage();
            }
            else
            {
                Console.WriteLine("\nYou Logged Out\n");
                break;
            }
        }
    }

    void CreateNewCandidate(Role candidateRole, List<User> _userList)
    {
        var fullName = Utils.GetStringInputUtil("Full name");
        var email = Utils.GetStringInputUtil("Email");

        Console.WriteLine($"\nNew {candidateRole.RoleName} for {fullName} with email {email} already created!");
    }

    void CreateNewPackage()
    {
        var packageName = Utils.GetStringInputUtil("Package name");

        Console.WriteLine("\nPackage " + packageName + " already created!");
    }
}
