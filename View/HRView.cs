namespace Bts.View;

using Bts.Constant;
using Bts.Model;
using Bts.Utils;
using Bts.IService;

internal class HRView
{
    readonly IUserService _userService;
    readonly IPackageService _packageService;
    User _hrUser;

    public HRView(IUserService userService, IPackageService packageService)
    {
        _userService = userService;
        _packageService = packageService;
    }

    public void MainMenu(User user)
    {
        _hrUser = user;
        while (true)
        {
            Console.WriteLine("\n=== Human Resource Menu ===");
            Console.WriteLine("1. Create New Candidate");
            Console.WriteLine("2. Show All Packages");
            Console.WriteLine("3. Create New Exam");
            Console.WriteLine("4. Show Exam List");
            Console.WriteLine("5. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 5);

            if (selectedOpt == 1)
            {
                CreateNewCandidate();
            }
            else if (selectedOpt == 2)
            {
                ShowPackageList();
            }
            else if (selectedOpt == 3)
            {
                CreateNewExam();
            }
            else if (selectedOpt == 4)
            {
                //ShowExamList(new List<Exam>());
            }
            else
            {
                Console.WriteLine("\nYou Logged Out\n");
                break;
            }
        }
    }

    void CreateNewCandidate()
    {
        var candidateRole = _userService.GetCandidateRole();
        var fullName = Utils.GetStringInputUtil("Full name");
        var email = Utils.GetStringInputUtil("Email");
        var newCandidate = new User()
        {
            FullName = fullName,
            Email = email,
            Pass = Utils.GenerateRandomAlphaNumericUtil(),
            Role = new Role() { Id = candidateRole.Id, },
            CreatedBy = _hrUser.Id,
            CreatedAt = DateTime.Now,
            Ver = 0,
            IsActive = true,
        };
        _userService.CreateUser(newCandidate);

        Console.WriteLine($"\nNew {candidateRole.RoleName} {fullName} with email {email} already created!");
    }

    void ShowPackageList()
    {
        while (true)
        {
            Console.WriteLine("\nPackage List");

            var packageList = _packageService.GetPackageList();
            var number = 1;
            foreach (var package in packageList)
            {
                Console.WriteLine($"{number}. {package.PackageCode} - {package.PackageName}");
                number++;
            }
            Console.WriteLine("\nPackage Menu :");
            Console.WriteLine("1. Create New Package");
            Console.WriteLine("2. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                CreateNewPackage();
            }
            else if (selectedOpt == 2)
            {
                return;
            }
        }
    }

    void CreateNewPackage()
    {
        var packageName = Utils.GetStringInputUtil("Package name");
        var newPackage = new Package()
        {
            PackageName = packageName,
            PackageCode = Utils.GenerateRandomAlphaNumericUtil(),
            CreatedBy = _hrUser.Id,
            CreatedAt = DateTime.Now,
            Ver = 0,
            IsActive = true,
        };
        _packageService.CreatePackage(newPackage);

        Console.WriteLine("\nPackage " + packageName + " successfully created!");
    }

    void CreateNewExam()
    {
        var candidateIndex = SelectCandidate();
        if (candidateIndex == -1) { return; }

        var reviewerIndex = SelectReviewer();
        if (reviewerIndex == -1) { return; }

        var packageIndex = SelectPackage();
        if (packageIndex == -1) { return; }

        Console.Write("Insert Login Start Date Time : ");
        var loginStartDatetime = Console.ReadLine();
        Console.Write("Insert Login End Date Time : ");
        var loginEndDatetime = Console.ReadLine();
        var duration = Utils.GetNumberInputUtil(30, 180, "Insert exam duration (in minutes)");

        Console.WriteLine("\nExam successfully created!");
    }

    int SelectCandidate()
    {
        Console.WriteLine("\nCandidate List");
        Console.WriteLine("1. Budiman");
        Console.WriteLine("2. Cancel");
        var selectedOpt = Utils.GetNumberInputUtil(1, 2, "Select Candidate");

        if (selectedOpt == 1)
        {
            return selectedOpt - 1;
        }
        else
        {
            Console.WriteLine("Create new exam cancelled");
            return -1;
        }
    }

    int SelectReviewer()
    {
        Console.WriteLine("\nReviewer List");
        Console.WriteLine("1. Andi");
        Console.WriteLine("2. Cancel");
        var selectedOpt = Utils.GetNumberInputUtil(1, 2, "Select Reviewer");

        if (selectedOpt == 1)
        {
            return selectedOpt - 1;
        }
        else
        {
            Console.WriteLine("Create new exam cancelled");
            return -1;
        }
    }

    int SelectPackage()
    {
        Console.WriteLine("\nPackage List");
        Console.WriteLine("1. Java OOP");
        Console.WriteLine("2. Cancel");
        var selectedOpt = Utils.GetNumberInputUtil(1, 2, "Select Package");

        if (selectedOpt == 1)
        {
            return selectedOpt - 1;
        }
        else
        {
            Console.WriteLine("Create new exam cancelled");
            return -1;
        }
    }
}
