namespace Bts.View;

using Bts.IService;
using Bts.Model;
using Bts.Utils;
using Bts.Constant;

internal class ReviewerView : BaseView
{
    readonly IPackageService _packageService;
    readonly IQuestionService _questionService;
    readonly IExamService _examService;
    readonly IDocumentService _documentService;

    User _reviewerUser;

    public ReviewerView(IPackageService packageService, IQuestionService questionService, IExamService examService, IDocumentService documentService)
    {
        _packageService = packageService;
        _questionService = questionService;
        _examService = examService;
        _documentService = documentService;
    }

    public void MainMenu(User user)
    {
        _reviewerUser = user;
        while (true)
        {
            Console.WriteLine("\n=== Reviewer Menu ===");
            Console.WriteLine("1. Show Package List");
            Console.WriteLine("2. Show Assigned Exam List");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                ShowPackageList();
            }
            else if (selectedOpt == 2)
            {
                ShowAssignedExamList();
            }
            else
            {
                Console.WriteLine("\nYou Logged Out\n");
                break;
            }
        }
    }

    void ShowPackageList()
    {
        while (true)
        {
            Console.WriteLine("\nYour Assigned Package List");
            var packageList = _packageService.GetPackageList();
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
                    number++;
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
        var noOfQuestion = Utils.GetNumberInputUtil(1, 20, "Insert how many questions to create");
        Console.WriteLine("\nSelect Question Type");
        Console.WriteLine("1. Multiple Choice");
        Console.WriteLine("2. Essay");
        Console.WriteLine("3. Back");
        var selectedOpt = Utils.GetNumberInputUtil(1, 3);

        if (selectedOpt == 1)
        {
            SelectMultipleChoiceQuestionType(package, noOfQuestion);
        }
        else if (selectedOpt == 2)
        {
            var questionList = new List<Question>();
            for (int i = 0; i < noOfQuestion; i++)
            {
                var questionText = Utils.GetStringInputUtil("\nEssay Question");
                var question = new Question()
                {
                    PackageId = package.Id,
                    QuestionContent = questionText,
                };
                questionList.Add(question);
            }
            _questionService.CreateQuestionList(questionList);
            Console.WriteLine($"\n{noOfQuestion} questions successfully added to {package.PackageName}!");
        }
    }

    void SelectMultipleChoiceQuestionType(Package package, int noOfQuestion)
    {
        Console.WriteLine("\nSelect Multiple Choice Question Type");
        Console.WriteLine("1. Text");
        Console.WriteLine("2. Image");
        Console.WriteLine("3. Cancel");
        var selectedOpt = Utils.GetNumberInputUtil(1, 3);

        var questionList = new List<Question>();
        if (selectedOpt == 1)
        {
            for (int i = 0; i < noOfQuestion; i++)
            {
                var questionText = Utils.GetStringInputUtil("\nMultiple Choice Question");
                var optionList = AddOption();
                if (optionList == null)
                {
                    return;
                }
                var question = new Question()
                {
                    PackageId = package.Id,
                    QuestionContent = questionText,
                    OptionList = optionList,
                };
                questionList.Add(question);
            }
            _questionService.CreateQuestionList(questionList);
            Console.WriteLine($"\n{noOfQuestion} questions successfully added to {package.PackageName}!");
        }
        else if (selectedOpt == 2)
        {
            for (int i = 0; i < noOfQuestion; i++)
            {
                var questionFilename = Utils.GetStringInputUtil("\nQuestion file name");
                var questionExtension = Utils.GetStringInputUtil("Question file extension");
                var optionList = AddOption();
                if (optionList == null)
                {
                    return;
                }
                var question = new Question()
                {
                    PackageId = package.Id,
                    Image = new BTSFile()
                    {
                        FileContent = questionFilename,
                        FileExtension = questionExtension,
                    },
                    OptionList = optionList,
                };
                questionList.Add(question);
            }
            _questionService.CreateQuestionList(questionList);
            Console.WriteLine($"\n{noOfQuestion} questions successfully added to {package.PackageName}!");
        }
    }

    List<MultipleChoiceOption>? AddOption()
    {
        var numberOfOption = Utils.GetNumberInputUtil(2, 20, "Insert how many options to create");

        Console.WriteLine("Select Multiple Choice Option Type");
        Console.WriteLine("1. Text");
        Console.WriteLine("2. Image");
        Console.WriteLine("3. Cancel");
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
                    },
                    IsCorrect = false,
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
        var examList = _examService.GetExamListByReviewer(_reviewerUser.Id);
        while (true)
        {
            Console.WriteLine("\nAssigned Exam List:");
            var number = 1;
            foreach (var exam in examList)
            {
                var candidateName = exam.Candidate.FullName;
                var packageName = exam.ExamPackage.Package.PackageName;
                var createdAt = exam.CreatedAt.ToString(ISODateTimeFormat);
                var acceptanceStatus = exam.AcceptanceStatus == null ? "None" : exam.AcceptanceStatus.StatusName;
                var submissionStatus = exam.ExamPackage.IsSubmitted == null ? "Not Attempted" : (bool)exam.ExamPackage.IsSubmitted ? "Submitted" : "On Progress";
                Console.WriteLine($"{number}. Candidate: {candidateName} | Package: {packageName} | Acc Status: {acceptanceStatus} | Submission Status: {submissionStatus} | {createdAt}");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number);

            if (selectedOpt == number)
            {
                return;
            }
            else
            {
                ShowExamDetail(examList[selectedOpt - 1]);
            }
        }
    }

    void ShowExamDetail(Exam exam)
    {
        while (true)
        {
            base.ShowExamDetail(exam.Id, _examService);

            Console.WriteLine("\nSelect Option");
            Console.WriteLine("1. Insert Score, Notes, and Acceptance Status");
            Console.WriteLine("2. Show Candidate's Document (CV & Transcript)");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                InsertScoreNotesAcceptanceStatus(exam);
            }
            else if (selectedOpt == 2)
            {
                ShowCandidateCVTranscript(exam);
            }
            else
            {
                break;
            }
        }
    }

    void InsertScoreNotesAcceptanceStatus(Exam exam)
    {
        Console.Write("\nEssay Score : ");
        var essayScore = Convert.ToDouble(Console.ReadLine());
        var notes = Utils.GetStringInputUtil("Insert Notes");

        var acceptanceStatusList = _examService.GetAcceptanceStatusList();
        var filteredStatusList = acceptanceStatusList
                                .Where(s => s.Code != AcceptanceStatusCode.NeedsReview)
                                .ToList();
        var number = 1;
        foreach (var acceptanceStatuse in filteredStatusList)
        {
            Console.WriteLine($"{number}. {acceptanceStatuse.StatusName}");
            number++;
        }
        var selectedOpt = Utils.GetNumberInputUtil(1, number - 1, "Select Acceptance Status");
        var selectedAcceptanceStatus = filteredStatusList[selectedOpt - 1];

        exam.AcceptanceStatusId = selectedAcceptanceStatus.Id;
        exam.ExamPackage.ReviewerNotes = notes;
        exam.ExamPackage.Exam = exam;
        exam.ExamPackage.ExamId = exam.Id;
        _examService.InsertScoreNotesAcceptanceStatusOnExamPackage(exam.ExamPackage, essayScore);
        Console.WriteLine("\nYour review successfully submitted");
    }

    void ShowCandidateCVTranscript(Exam exam)
    {
        while (true)
        {
            var documentList = _documentService.GetCandidateCVAndTranscript(exam.CandidateId);
            var number = 1;
            foreach (var document in documentList)
            {
                Console.WriteLine($"{number}. {document.DocumentType.TypeName}");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedDocNumber = Utils.GetNumberInputUtil(1, number, "Select Document");

            if (selectedDocNumber == number)
            {
                break;
            }
            else
            {
                var selectedDoc = documentList[selectedDocNumber - 1];
                Console.WriteLine($"\n{selectedDoc.DocumentType.TypeName} : {selectedDoc.File.FileContent}.{selectedDoc.File.FileExtension}\n");
            }
        }
    }
}
