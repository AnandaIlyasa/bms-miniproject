namespace bms.View;

using bms.Model;
using bms.Utils;

internal class ReviewerView
{
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== Reviewer Menu ===");
            Console.WriteLine("1. Assigned Package List");
            Console.WriteLine("2. Review Exam");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                ShowQuestionPackageList();
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

    void ShowQuestionPackageList()
    {
        while (true)
        {
            Console.WriteLine("\nYour Question Package List");
            Console.WriteLine("1. Package Java");
            Console.WriteLine("2. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                ShowQuestionList();
            }
            else
            {
                break;
            }
        }
    }

    void ShowQuestionList()
    {
        while (true)
        {
            Console.WriteLine("\nJava Package Question List");
            Console.WriteLine("1. Add New Question");
            Console.WriteLine("2. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                AddNewQuestion();
            }
            else
            {
                break;
            }
        }
    }

    void AddNewQuestion()
    {
        while (true)
        {
            Console.WriteLine("\nSelect Question Type");
            Console.WriteLine("1. Multiple Choice");
            Console.WriteLine("2. Essay");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            string question;
            if (selectedOpt == 1)
            {
                question = Utils.GetStringInputUtil("Multiple Choice Question");
                var optionList = AddOption();
            }
            else if (selectedOpt == 2)
            {
                question = Utils.GetStringInputUtil("Essay Question");
            }
            else
            {
                break;
            }

            Console.WriteLine("\nQuestion successfully added!");
        }
    }

    List<MultipleChoiceOption> AddOption()
    {
        List<MultipleChoiceOption> multipleChoiceOptionList = new List<MultipleChoiceOption>();
        var optionA = Utils.GetStringInputUtil("Insert option A");
        var optionB = Utils.GetStringInputUtil("Insert option B");
        var optionC = Utils.GetStringInputUtil("Insert option C");
        var optionD = Utils.GetStringInputUtil("Insert option D");

        return multipleChoiceOptionList;
    }

    void ShowExamList()
    {
        while (true)
        {
            Console.WriteLine("\nYour Exam List");
            Console.WriteLine("1. Exam Java (12-12-2023 11:00 - 13:00) Candidate Name : Budiman");
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
            Console.WriteLine("---- Exam Java (12-12-2023 11:00 - 13:00) ----");
            Console.WriteLine("Candidate name : Budiman");
            Console.WriteLine("Multiple choice score : 70 / 100\n");
            Console.WriteLine("Essay answers : ");
            Console.WriteLine("1. Sebutkan prinsip OOP ke-1");
            Console.WriteLine("Jawaban : Inheritance");
            Console.WriteLine("Score : 0");
            Console.WriteLine("Notes : ");
            Console.WriteLine("2. Sebutkan prinsip OOP ke-2");
            Console.WriteLine("Jawaban : Encapsulation");
            Console.WriteLine("Score : 0");
            Console.WriteLine("Notes : ");
            Console.WriteLine("3. Sebutkan prinsip OOP ke-3");
            Console.WriteLine("Jawaban : Abstraction");
            Console.WriteLine("Score : 0");
            Console.WriteLine("Notes : ");
            Console.WriteLine("4. Sebutkan prinsip OOP ke-4");
            Console.WriteLine("Jawaban : Polymorphism");
            Console.WriteLine("Score : 0");
            Console.WriteLine("Notes : ");
            Console.WriteLine("Total Essay Score : 0");
            Console.WriteLine("5. Back");
            var selectedEssay = Utils.GetNumberInputUtil(1, 5, "Select Essay Number");

            if (selectedEssay == 5)
            {
                break;
            }
            else
            {
                var score = Utils.GetNumberInputUtil(0, 100, "Insert Score");
                var notes = Utils.GetStringInputUtil("Insert Notes");
            }
        }
    }
}
