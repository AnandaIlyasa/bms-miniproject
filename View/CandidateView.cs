namespace bms.View;

using bms.Utils;

internal class CandidateView
{
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== Candidate Menu ===");
            Console.WriteLine("1. Show Document List");
            Console.WriteLine("2. Show Exam");
            //Console.WriteLine("3. Show Exam Result");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                ShowDocumentList();
            }
            else if (selectedOpt == 2)
            {
                ShowExam();
            }
            else
            {
                Console.WriteLine("\nYou Logged Out\n");
                break;
            }
        }
    }

    void ShowDocumentList()
    {
        while (true)
        {
            Console.WriteLine("\nDocument List");
            Console.WriteLine("1. CV (uploaded)");
            Console.WriteLine("2. Ijazah (uploaded)");
            Console.WriteLine("3. Transcript (uploaded)");
            Console.WriteLine("4. KK (not uploaded)");
            Console.WriteLine("5. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 5);

            if (selectedOpt == 5)
            {
                break;
            }
            else
            {
                Console.WriteLine("Oops, this feature is under development");
            }
        }
    }

    void ShowExam()
    {
        Console.WriteLine("---- Exam Java (12-12-2023 11:00 - 13:00) ----");
        Console.WriteLine("Candidate name : Budiman");
        Console.WriteLine("Acceptance status : (exam not yet submitted)");

        Console.WriteLine("Total Essay Score : 0");
    }
}
