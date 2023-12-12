namespace Bts.View;

using Bts.Utils;

abstract class BaseView
{
    public static void ShowExamList()
    {
        while (true)
        {
            Console.WriteLine("\nExam List");
            Console.WriteLine("1. Candidate Name : Budiman | JAVA-01 | Submitted | Needs review | 12-12-2023 11:00 - 13:00");
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

    public static void ShowExamDetail()
    {
        Console.WriteLine("\n---- JAVA-01 (12-12-2023 11:00 - 13:00) ----");
        Console.WriteLine("Candidate name : Budiman");
        Console.WriteLine("Submission status : Submitted");
        Console.WriteLine("Acceptance status : Needs review");
        Console.WriteLine("Multiple choice score : 70 / 100\n");
        Console.WriteLine("Essay score : 80.25");
        Console.WriteLine("Essay notes : you're doing great!");
        Console.WriteLine("Candidate's essay answers : ");
        Console.WriteLine("1. Sebutkan prinsip OOP ke-1");
        Console.WriteLine("Jawaban : Inheritance");
        Console.WriteLine("2. Sebutkan prinsip OOP ke-2");
        Console.WriteLine("Jawaban : Encapsulation");
        Console.WriteLine("3. Sebutkan prinsip OOP ke-3");
        Console.WriteLine("Jawaban : Abstraction");
        Console.WriteLine("4. Sebutkan prinsip OOP ke-4");
        Console.WriteLine("Jawaban : Polymorphism");
    }
}
