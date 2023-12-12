namespace Bts.View;

using Bts.Utils;
using Bts.Model;

internal class CandidateView
{
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\nYou must upload all these documents first");
            Console.WriteLine("1. CV (not uploaded)");
            Console.WriteLine("2. Ijazah (not uploaded)");
            Console.WriteLine("3. Transcript (not uploaded)");
            Console.WriteLine("4. KK (not uploaded)");
            var selectedOpt = Utils.GetNumberInputUtil(1, 5);

            if (selectedOpt == 1)
            {
                var cvFilename = Utils.GetStringInputUtil("CV file name");
                var cvExtension = Utils.GetStringInputUtil("CV file extension");
            }
            else if (selectedOpt == 2)
            {
                var ijazahFilename = Utils.GetStringInputUtil("Ijazah file name");
                var ijazahExtension = Utils.GetStringInputUtil("Ijazah file extension");
            }
            else if (selectedOpt == 3)
            {
                var transcriptFilename = Utils.GetStringInputUtil("Transcript file name");
                var transcriptExtension = Utils.GetStringInputUtil("Transcript file extension");
            }
            else if (selectedOpt == 4)
            {
                var kkFilename = Utils.GetStringInputUtil("KK file name");
                var kkExtension = Utils.GetStringInputUtil("KK file extension");
                ShowExam();
                break;
            }
        }
    }

    void ShowExam()
    {
        Console.WriteLine("\n1. Start Exam JAVA-01 (Duration : 60 minutes)");
        var selectedOpt = Utils.GetNumberInputUtil(1, 1);

        if (selectedOpt == 1)
        {
            StartExam();
        }
    }

    void StartExam()
    {
        while (true)
        {
            Console.WriteLine("\n---- JAVA-01 (Time remaining: 54 minutes) ----");
            Console.WriteLine("Candidate name : Budiman");
            Console.WriteLine("1. Manakah prinsip OOP ke-1");
            Console.WriteLine("A. Inheritance");
            Console.WriteLine("B. Encapsulation");
            Console.WriteLine("C. Abstraction");
            Console.WriteLine("D. Polymorphism");
            Console.WriteLine("2. Sebutkan prinsip OOP ke-2");
            Console.WriteLine("3. Finish and Submit");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3, "Select question number to answer");

            if (selectedOpt == 1)
            {
                var candidateOpt = Utils.GetStringInputUtil("Your option");
            }
            else if (selectedOpt == 2)
            {
                var candidateAnswer = Utils.GetStringInputUtil("Your answer");
            }
            else
            {
                Console.WriteLine("\nYou have finished the exam!");
                break;
            }
        }
    }
}
