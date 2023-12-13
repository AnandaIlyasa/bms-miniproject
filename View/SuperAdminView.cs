namespace Bts.View;

using Bts.Utils;
using Bts.Model;
using Bts.Constant;
using Bts.IService;
using Bts.Service;

internal class SuperAdminView
{
    readonly IUserService _userService;
    readonly IExamService _examService;
    User _superadminUser;

    readonly string ISODateTimeFormat = "yyyy-MM-dd HH:mm:ss";

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
                ShowExamList();
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
            Role = new Role() { Id = roleId, },
            CreatedBy = _superadminUser.Id,
            CreatedAt = DateTime.Now,
            Ver = 0,
            IsActive = true,
        };

        _userService.CreateUser(newUser);

        var createdRoleName = roleList.Find(role => role.Id == roleId)!.RoleName;
        Console.WriteLine($"\nNew {createdRoleName} for {fullName} with email {email} already created!");
    }

    public void ShowExamList()
    {
        var examList = _examService.GetExamList();
        while (true)
        {
            Console.WriteLine("\nExam List");
            var number = 1;
            foreach (var exam in examList)
            {
                var candidateName = exam.Candidate.FullName;
                var packageName = exam.Package.PackageName;
                var createdAt = exam.CreatedAt.ToString(ISODateTimeFormat);
                var acceptanceStatus = exam.AcceptanceStatus!.StatusName == "" ? "None" : exam.AcceptanceStatus.StatusName;
                var submissionStatus = exam.ExamPackage.IsSubmitted == null ? "Not Attempted" : (bool)exam.ExamPackage.IsSubmitted ? "Submitted" : "On Progress";
                Console.WriteLine($"{number}. Candidate: {candidateName} | Package: {packageName} | Acc Status: {acceptanceStatus} | Submission Status: {submissionStatus} | {createdAt}");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 6);

            if (selectedOpt == number)
            {
                return;
            }
            else
            {
                ShowExamDetail(examList[selectedOpt - 1].Id);
            }
        }
    }

    public void ShowExamDetail(int examId)
    {
        var exam = _examService.GetExamById(examId);
        var packageName = "java";
        var examStartDateTime = exam?.ExamPackage.ExamStartDateTime == null ? "Not Attempted" : exam.ExamPackage.ExamStartDateTime?.ToString(ISODateTimeFormat);
        var submissionStatus = exam?.ExamPackage.IsSubmitted == null ? "Not Attempted" : (bool)exam.ExamPackage.IsSubmitted ? "Submitted" : "On Progress";
        var acceptanceStatus = exam?.AcceptanceStatus!.StatusName == "" ? "None" : exam.AcceptanceStatus.StatusName;
        Console.WriteLine($"\n---- {packageName} ({examStartDateTime}) ----");
        Console.WriteLine("Candidate: " + exam.Candidate.FullName);
        Console.WriteLine("Submission status: " + submissionStatus);
        Console.WriteLine("Acceptance status: " + acceptanceStatus);
        Console.WriteLine("Score: " + exam.ExamPackage.ReviewerScore);
        Console.WriteLine("Essay notes: " + exam.ExamPackage.ReviewerNotes);

        Console.WriteLine("\nCandidate's exam answers:");
        var number = 1;
        var groupedAnswerList = exam.CandidateAnswerList
            .OrderByDescending(answer => answer.ChoiceOption?.OptionChar)
            .ToList();
        foreach (var answer in groupedAnswerList)
        {
            var question = answer.Question.QuestionContent == null ? answer.Question.Image?.FileContent + "." + answer.Question.Image?.FileExtension : answer.Question.QuestionContent;
            var candidateAnswer = answer.AnswerContent ??
                                  "(" + answer.ChoiceOption!.OptionChar + ") " +
                                  (answer.ChoiceOption.OptionText ?? answer.ChoiceOption.OptionImage!.FileContent + answer.ChoiceOption.OptionImage.FileExtension);
            Console.WriteLine($"{number}. {question}");
            Console.WriteLine("   Answer: " + candidateAnswer);
            number++;
        }
    }
}
