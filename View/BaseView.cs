namespace Bts.View;

using Bts.Utils;
using Bts.IService;
using Bts.Model;

abstract class BaseView
{
    public readonly string ISODateTimeFormat = "yyyy-MM-dd HH:mm";

    public void ShowExamList(IExamService examService)
    {
        var examList = examService.GetExamList();
        while (true)
        {
            Console.WriteLine("\nExam List");
            var number = 1;
            foreach (var exam in examList)
            {
                var createdAt = exam.CreatedAt.ToString(ISODateTimeFormat);
                var acceptanceStatus = exam.AcceptanceStatus is null ? "None" : exam.AcceptanceStatus.StatusName;
                var submissionStatus = exam.ExamPackage.IsSubmitted == null ? "Not Attempted" :
                    exam.ExamPackage.IsSubmitted == false ? "In Progress" : "Submitted";
                Console.WriteLine($"{number}. Candidate: {exam.Candidate.FullName} | Reviewer: {exam.Reviewer.FullName} | Package: {exam.ExamPackage.Package.PackageName} | Acc Status: {acceptanceStatus} | Submission Status: {submissionStatus} | Created at: {createdAt}");
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
                ShowExamDetail(examList[selectedOpt - 1].Id, examService);
            }
        }
    }

    public void ShowExamDetail(int examId, IExamService examService)
    {
        var exam = examService.GetExamById(examId);
        var packageName = exam.ExamPackage.Package.PackageName;
        var examStartDateTime = exam?.ExamPackage.ExamStartDateTime == null ? "Not Attempted" : exam.ExamPackage.ExamStartDateTime?.ToString(ISODateTimeFormat);
        var submissionStatus = exam?.ExamPackage.IsSubmitted == null ? "Not Attempted" : (bool)exam.ExamPackage.IsSubmitted ? "Submitted" : "On Progress";
        var acceptanceStatus = exam?.AcceptanceStatus == null ? "None" : exam.AcceptanceStatus.StatusName;
        Console.WriteLine($"\n---- {packageName} ({examStartDateTime}) ----");
        Console.WriteLine("Candidate: " + exam.Candidate.FullName);
        Console.WriteLine("Submission status: " + submissionStatus);
        Console.WriteLine("Acceptance status: " + acceptanceStatus);
        Console.WriteLine("Score: " + exam.ExamPackage.ReviewerScore);
        Console.WriteLine("Essay notes: " + exam.ExamPackage.ReviewerNotes);

        Console.WriteLine("\nCandidate's exam answers:");
        var number = 1;

        foreach (var question in exam.QuestionList)
        {
            var questionString = question.QuestionContent == null ? question.Image?.FileContent + "." + question.Image?.FileExtension : question.QuestionContent;
            var answer = exam.CandidateAnswerList.Find(ca => ca.QuestionId == question.Id);
            string answerString = "";
            if (answer == null)
            {
                answerString = "[Not Answered]";
            }
            else if (answer.AnswerContent != null && answer.AnswerContent != "")
            {
                answerString = answer.AnswerContent;
            }
            else
            {
                answerString = "(" + answer.ChoiceOption?.OptionChar + ") ";
                if (answer.ChoiceOption?.OptionText != null && answer.ChoiceOption.OptionText != "")
                {
                    answerString += answer.ChoiceOption.OptionText;
                }
                else
                {
                    answerString += answer.ChoiceOption?.OptionImage?.FileContent + "." + answer.ChoiceOption?.OptionImage?.FileExtension;
                }
            }

            Console.Write($"{number}. {questionString}");
            Console.WriteLine(" - Answer: " + answerString);
            number++;
        }
    }
}
