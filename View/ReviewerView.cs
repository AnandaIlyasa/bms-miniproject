namespace Bts.View;

using Bts.Model;
using Bts.Utils;

internal class ReviewerView
{
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== Reviewer Menu ===");
            Console.WriteLine("1. Assigned Package List");
            Console.WriteLine("2. Show Exam List");
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

            if (selectedOpt == 1)
            {
                SelectMultipleChoiceQuestionType();
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

    void SelectMultipleChoiceQuestionType()
    {
        while (true)
        {
            Console.WriteLine("\nSelect Multiple Choice Question Type");
            Console.WriteLine("1. Text");
            Console.WriteLine("2. Image");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            string question;
            if (selectedOpt == 1)
            {
                var noOfQuestion = Utils.GetNumberInputUtil(1, 20, "Insert how many questions to create");
                for (int i = 0; i < noOfQuestion; i++)
                {
                    question = Utils.GetStringInputUtil("Multiple Choice Question");
                    AddOption(question);
                }
            }
            else if (selectedOpt == 2)
            {
                var questionFilename = Utils.GetStringInputUtil("Question file name");
                var questionExtension = Utils.GetStringInputUtil("Question file extension");
                question = questionFilename + "." + questionExtension;
                AddOption(question);
            }
            else
            {
                break;
            }
        }
    }

    void AddOption(string question)
    {
        var numberOfOption = Utils.GetNumberInputUtil(2, 20, "Insert how many options to create");

        Console.WriteLine("Select Multiple Choice Option Type");
        Console.WriteLine("1. Text");
        Console.WriteLine("2. Image");
        Console.WriteLine("3. Back");
        var selectedOpt = Utils.GetNumberInputUtil(1, 3);

        List<MultipleChoiceOption> multipleChoiceOptionList = new List<MultipleChoiceOption>();
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
                multipleChoiceOptionList.Add(option);
            }
            Console.WriteLine("\nOption list :");
            Console.WriteLine("1. A) Inheritance");
            Console.WriteLine("2. B) Encapsulation");
            var correctOptionId = Utils.GetNumberInputUtil(1, 2, "Select the correct option");
        }
        else if (selectedOpt == 2)
        {
            for (int i = 0; i < numberOfOption; i++)
            {
                var optionChar = Convert.ToChar(i + 65);
                var optionFilename = Utils.GetStringInputUtil("Insert option " + optionChar + " file name");
                var optionExtension = Utils.GetStringInputUtil("Insert option " + optionChar + " file extension");
                var optionContent = optionFilename + "." + optionExtension;
            }
            Console.WriteLine("\nOption list :");
            Console.WriteLine("1. A) Inheritance");
            Console.WriteLine("2. B) Encapsulation");
            var correctOptionId = Utils.GetNumberInputUtil(1, 2, "Select the correct option");
        }
        else
        {
            return;
        }
    }

    void ShowExamList()
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
            Console.WriteLine("2. Back");
            var selectedEssay = Utils.GetNumberInputUtil(1, 2);

            if (selectedEssay == 1)
            {
                Console.Write("Score : ");
                var score = (float)Convert.ToDouble(Console.ReadLine());
                var notes = Utils.GetStringInputUtil("Insert Notes");
                Console.WriteLine("score : " + score);
            }
            else
            {
                break;
            }
        }
    }
}
