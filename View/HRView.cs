namespace Bts.View;

using Bts.Constant;
using Bts.Model;
using Bts.Utils;
using Bts.IService;

internal class HRView : BaseView
{
    readonly IUserService _userService;
    readonly IPackageService _packageService;
    readonly IExamService _examService;

    User _hrUser;

    public HRView(IUserService userService, IPackageService packageService, IExamService examService)
    {
        _userService = userService;
        _packageService = packageService;
        _examService = examService;
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
                ShowExamList(_examService);
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

        Console.WriteLine($"\nNew {candidateRole.RoleName} {fullName} with email {email} successfully created!");
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
        var candidate = SelectCandidate();
        if (candidate == null) { return; }

        var reviewer = SelectReviewer();
        if (reviewer == null) { return; }

        var package = SelectPackage();
        if (package == null) { return; }

        var loginStartDatetime = Utils.GetDateTimeInputUtil("Insert Login Start Date Time", ISODateTimeFormat);
        var loginEndDatetime = Utils.GetDateTimeInputUtil("Insert Login End Date Time", ISODateTimeFormat);
        var duration = Utils.GetNumberInputUtil(30, 180, "Insert exam duration (in minutes)");

        var exam = new Exam()
        {
            Candidate = candidate,
            Reviewer = reviewer,
            LoginStart = loginStartDatetime,
            LoginEnd = loginEndDatetime,
            CreatedBy = _hrUser.Id,
            CreatedAt = DateTime.Now,
            Ver = 0,
            IsActive = true,
        };

        var examPackage = new ExamPackage()
        {
            Package = package,
            Exam = exam,
            Duration = duration,
            CreatedBy = _hrUser.Id,
            CreatedAt = DateTime.Now,
            Ver = 0,
            IsActive = true,
        };

        _examService.CreateExam(examPackage);

        Console.WriteLine("\nExam successfully created!");
    }

    User? SelectCandidate()
    {
        Console.WriteLine("\nCandidate List");
        var candidateList = _userService.GetCandidateList();
        var number = 1;
        foreach (var candidate in candidateList)
        {
            Console.WriteLine($"{number}. {candidate.FullName} ({candidate.Email})");
            number++;
        }
        Console.WriteLine(number + ". Cancel");
        var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Candidate");

        if (selectedOpt == number)
        {
            Console.WriteLine("Create new exam cancelled");
            return null;
        }
        else
        {
            return candidateList[selectedOpt - 1];
        }
    }

    User? SelectReviewer()
    {
        Console.WriteLine("\nReviewer List");
        var reviewerList = _userService.GetReviewerList();
        var number = 1;
        foreach (var reviewer in reviewerList)
        {
            Console.WriteLine($"{number}. {reviewer.FullName} ({reviewer.Email})");
            number++;
        }
        Console.WriteLine(number + ". Cancel");
        var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Reviewer");

        if (selectedOpt == number)
        {
            Console.WriteLine("Create new exam cancelled");
            return null;
        }
        else
        {
            return reviewerList[selectedOpt - 1];
        }
    }

    Package? SelectPackage()
    {
        Console.WriteLine("\nPackage List");
        var packageList = _packageService.GetPackageList();
        var number = 1;
        foreach (var package in packageList)
        {
            Console.WriteLine($"{number}. {package.PackageName} ({package.PackageCode})");
            number++;
        }
        Console.WriteLine(number + ". Cancel");
        var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Package");

        if (selectedOpt == number)
        {
            Console.WriteLine("Create new exam cancelled");
            return null;
        }
        else
        {
            return packageList[selectedOpt - 1];
        }
    }
}
