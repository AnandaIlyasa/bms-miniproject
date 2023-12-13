//namespace Bts.View;

//using Bts.Utils;
//using Bts.Model;

//abstract class BaseView
//{
//    public void ShowExamList(List<Exam> examList)
//    {
//        while (true)
//        {
//            Console.WriteLine("\nExam List");
//            var number = 1;
//            foreach (var exam in examList)
//            {
//                var candidateName = exam.Candidate.FullName;
//                var packageName = exam.Package.PackageName;
//                var createdAt = exam.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
//                var acceptanceStatus = exam.AcceptanceStatus!.StatusName == "" ? "None" : exam.AcceptanceStatus.StatusName;
//                var submissionStatus = exam.ExamPackage.IsSubmitted == null ? "Not Done" : (bool)exam.ExamPackage.IsSubmitted ? "Submitted" : "On Progress";
//                Console.WriteLine($"{number}. Candidate: {candidateName} | Package: {packageName} | Acc Status: {acceptanceStatus} | Submission Status: {submissionStatus} | {createdAt}");
//                number++;
//            }
//            Console.WriteLine(number + ". Back");
//            var selectedOpt = Utils.GetNumberInputUtil(1, 6);

//            if (selectedOpt == number)
//            {
//                return;
//            }
//            else
//            {
//                ShowExamDetail(examList[selectedOpt - 1].Id);
//            }
//        }
//    }

//    public void ShowExamDetail(int examId)
//    {

//        Console.WriteLine("\n---- JAVA-01 (12-12-2023 11:00 - 13:00) ----");
//        Console.WriteLine("Candidate name : Budiman");
//        Console.WriteLine("Submission status : Submitted");
//        Console.WriteLine("Acceptance status : Needs review");
//        Console.WriteLine("Multiple choice score : 70 / 100\n");
//        Console.WriteLine("Essay score : 80.25");
//        Console.WriteLine("Essay notes : you're doing great!");
//        Console.WriteLine("Candidate's essay answers : ");
//        Console.WriteLine("1. Sebutkan prinsip OOP ke-1");
//        Console.WriteLine("Jawaban : Inheritance");
//        Console.WriteLine("2. Sebutkan prinsip OOP ke-2");
//        Console.WriteLine("Jawaban : Encapsulation");
//        Console.WriteLine("3. Sebutkan prinsip OOP ke-3");
//        Console.WriteLine("Jawaban : Abstraction");
//        Console.WriteLine("4. Sebutkan prinsip OOP ke-4");
//        Console.WriteLine("Jawaban : Polymorphism");
//    }
//}
