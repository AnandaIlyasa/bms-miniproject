namespace Bts.View;

using Bts.IService;
using Bts.Model;
using Bts.Utils;

internal class ReviewerView : BaseView
{
    readonly IPackageService _packageService;
    readonly IQuestionService _questionService;

    User _reviewerUser;

    public ReviewerView(IPackageService packageService, IQuestionService questionService)
    {
        _packageService = packageService;
        _questionService = questionService;
    }

    public void MainMenu(User user)
    {
        _reviewerUser = user;
        while (true)
        {
            Console.WriteLine("\n=== Reviewer Menu ===");
            Console.WriteLine("1. Assigned Package List");
            Console.WriteLine("2. Assigned Exam List");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                ShowAssignedPackageList();
            }
            else if (selectedOpt == 2)
            {
                ShowAssignedExamList(); // NOT DONE
            }
            else
            {
                Console.WriteLine("\nYou Logged Out\n");
                break;
            }
        }
    }

    void ShowAssignedPackageList()
    {
        while (true)
        {
            Console.WriteLine("\nYour Assigned Package List");
            var packageList = _packageService.GetPackageListByReviewer(_reviewerUser);
            var number = 1;
            foreach (var package in packageList)
            {
                Console.WriteLine($"{number}. {package.PackageName} ({package.PackageCode})");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Package");

            if (selectedOpt == number)
            {
                break;
            }
            else
            {
                ShowQuestionList(packageList[selectedOpt - 1]);
            }
        }
    }

    void ShowQuestionList(Package package)
    {
        while (true)
        {
            Console.WriteLine("\n" + package.PackageName + " Question List:");
            var questionList = _questionService.GetQuestionListByPackage(package.Id);
            var number = 1;
            foreach (var question in questionList)
            {
                var questionImage = question.Image;
                var questionText = question.QuestionContent;
                var questionString = questionImage?.FileContent == null ? questionText : questionImage?.FileContent + "." + questionImage?.FileExtension;
                Console.WriteLine($"{number}. {questionString}");

                var optionList = question.OptionList;
                if (optionList == null || optionList.Count == 0)
                {
                    continue;
                }
                else
                {
                    foreach (var option in optionList)
                    {
                        var optChar = option.OptionChar;
                        var optText = option.OptionText;
                        var optImage = option.OptionImage;
                        var optString = optImage?.FileContent == null ? optText : optImage?.FileContent + "." + optImage?.FileExtension;
                        Console.WriteLine($"   {optChar}) {optString}");
                    }
                }
                number++;
            }
            Console.WriteLine("\n1. Add Question");
            Console.WriteLine("2. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                AddNewQuestion(package);
            }
            else
            {
                break;
            }
        }
    }

    void AddNewQuestion(Package package)
    {
        while (true)
        {
            Console.WriteLine("\nSelect Question Type");
            Console.WriteLine("1. Multiple Choice");
            Console.WriteLine("2. Essay");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                var success = SelectMultipleChoiceQuestionType(package);
                if (success)
                {
                    break;
                }
            }
            else if (selectedOpt == 2)
            {
                var question = Utils.GetStringInputUtil("Essay Question");
            }
            else
            {
                break;
            }

            Console.WriteLine("\nQuestions successfully added!");
        }
    }

    bool SelectMultipleChoiceQuestionType(Package package)
    {
        while (true)
        {
            Console.WriteLine("\nSelect Multiple Choice Question Type");
            Console.WriteLine("1. Text");
            Console.WriteLine("2. Image");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            var questionList = new List<Question>();
            if (selectedOpt == 1)
            {
                var noOfQuestion = Utils.GetNumberInputUtil(1, 20, "Insert how many questions to create");
                for (int i = 0; i < noOfQuestion; i++)
                {
                    var questionText = Utils.GetStringInputUtil("Multiple Choice Question");
                    var optionList = AddOption();
                    if (optionList == null)
                    {
                        break;
                    }
                    var question = new Question()
                    {
                        Package = package,
                        QuestionContent = questionText,
                        OptionList = optionList,
                        CreatedBy = _reviewerUser.Id,
                        CreatedAt = DateTime.Now,
                        Ver = 0,
                        IsActive = true,
                    };
                    questionList.Add(question);
                }
                _questionService.CreateQuestionList(questionList);
                Console.WriteLine($"\n{noOfQuestion} questions successfully added to {package.PackageName}!");
                return true;
            }
            else if (selectedOpt == 2)
            {
                var noOfQuestion = Utils.GetNumberInputUtil(1, 20, "Insert how many questions to create");
                for (int i = 0; i < noOfQuestion; i++)
                {
                    var questionFilename = Utils.GetStringInputUtil("Question file name");
                    var questionExtension = Utils.GetStringInputUtil("Question file extension");
                    var optionList = AddOption();
                    if (optionList == null)
                    {
                        break;
                    }
                    var question = new Question()
                    {
                        Package = package,
                        Image = new BTSFile()
                        {
                            FileContent = questionFilename,
                            FileExtension = questionExtension,
                            CreatedBy = _reviewerUser.Id,
                            CreatedAt = DateTime.Now,
                            Ver = 0,
                            IsActive = true,
                        },
                        OptionList = optionList,
                        CreatedBy = _reviewerUser.Id,
                        CreatedAt = DateTime.Now,
                        Ver = 0,
                        IsActive = true,
                    };
                    questionList.Add(question);
                }
                _questionService.CreateQuestionList(questionList);
                Console.WriteLine($"\n{noOfQuestion} questions successfully added to {package.PackageName}!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    List<MultipleChoiceOption>? AddOption()
    {
        var numberOfOption = Utils.GetNumberInputUtil(2, 20, "Insert how many options to create");

        Console.WriteLine("Select Multiple Choice Option Type");
        Console.WriteLine("1. Text");
        Console.WriteLine("2. Image");
        Console.WriteLine("3. Back");
        var selectedOpt = Utils.GetNumberInputUtil(1, 3);

        List<MultipleChoiceOption> optionList = new List<MultipleChoiceOption>();
        if (selectedOpt == 1)
        {
            for (int i = 0; i < numberOfOption; i++)
            {
                var optionChar = Convert.ToChar(i + 65).ToString();
                var optionContent = Utils.GetStringInputUtil("Insert option " + optionChar);
                var option = new MultipleChoiceOption()
                {
                    OptionChar = optionChar,
                    OptionText = optionContent,
                    IsCorrect = false,
                    CreatedBy = _reviewerUser.Id,
                    CreatedAt = DateTime.Now,
                    Ver = 0,
                    IsActive = true,
                };
                optionList.Add(option);
            }
            Console.WriteLine("\nOption list :");
            var number = 1;
            foreach (var option in optionList)
            {
                Console.WriteLine($"{number}. {option.OptionChar}) {option.OptionText}");
                number++;
            }
            var correctOptionNo = Utils.GetNumberInputUtil(1, number - 1, "Select the correct option");
            optionList[correctOptionNo - 1].IsCorrect = true;
            return optionList;
        }
        else if (selectedOpt == 2)
        {
            for (int i = 0; i < numberOfOption; i++)
            {
                var optionChar = Convert.ToChar(i + 65).ToString();
                var optionFilename = Utils.GetStringInputUtil("Insert option " + optionChar + " file name");
                var optionExtension = Utils.GetStringInputUtil("Insert option " + optionChar + " file extension");
                var option = new MultipleChoiceOption()
                {
                    OptionChar = optionChar,
                    OptionImage = new BTSFile()
                    {
                        FileContent = optionFilename,
                        FileExtension = optionExtension,
                        CreatedBy = _reviewerUser.Id,
                        CreatedAt = DateTime.Now,
                        Ver = 0,
                        IsActive = true,
                    },
                    IsCorrect = false,
                    CreatedBy = _reviewerUser.Id,
                    CreatedAt = DateTime.Now,
                    Ver = 0,
                    IsActive = true,
                };
                optionList.Add(option);
            }
            Console.WriteLine("\nOption list :");
            var number = 1;
            foreach (var option in optionList)
            {
                Console.WriteLine($"{number}. {option.OptionChar}) {option.OptionImage?.FileContent}.{option.OptionImage?.FileExtension}");
                number++;
            }
            var correctOptionNo = Utils.GetNumberInputUtil(1, number - 1, "Select the correct option");
            optionList[correctOptionNo - 1].IsCorrect = true;
            return optionList;
        }
        else
        {
            return null;
        }
    }

    void ShowAssignedExamList()
    {
        while (true)
        {
            Console.WriteLine("\nExam List");
            Console.WriteLine("1. JAVA-01 | Submitted | Needs review | Candidate Name : Budiman | 12-12-2023 11:00 - 13:00");
            Console.WriteLine("2. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                ShowExamDetail();
            }
            else
            {
                return;
            }
        }
    }

    void ShowExamDetail()
    {
        while (true)
        {
            Console.WriteLine("\n---- JAVA-01 (12-12-2023 11:00 - 13:00) ----");
            Console.WriteLine("Candidate name : Budiman");
            Console.WriteLine("Submission status : Submitted");
            Console.WriteLine("Acceptance status : Needs review");
            Console.WriteLine("Multiple choice score : 70 / 100\n");
            Console.WriteLine("Essay answers : ");
            Console.WriteLine("1. Sebutkan prinsip OOP ke-1");
            Console.WriteLine("Jawaban : Inheritance");
            Console.WriteLine("2. Sebutkan prinsip OOP ke-2");
            Console.WriteLine("Jawaban : Encapsulation");
            Console.WriteLine("3. Sebutkan prinsip OOP ke-3");
            Console.WriteLine("Jawaban : Abstraction");
            Console.WriteLine("4. Sebutkan prinsip OOP ke-4");
            Console.WriteLine("Jawaban : Polymorphism");

            Console.WriteLine("\nSelect Option");
            Console.WriteLine("1. Insert Score and Notes");
            Console.WriteLine("2. Show Candidate's Document (CV & Transcript)");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                Console.Write("Score : ");
                var score = (float)Convert.ToDouble(Console.ReadLine());
                var notes = Utils.GetStringInputUtil("Insert Notes");
                Console.WriteLine("score : " + score);
            }
            else if (selectedOpt == 2)
            {

            }
            else
            {
                break;
            }
        }
    }
}
