using bms.View;

namespace quiz_week4_day1_batch16_anandailyasaputra
{
    internal class Program
    {
        static void Main()
        {
            var authView = new AuthView(new SuperAdminView(), new CandidateView(), new HRView(), new ReviewerView());
            authView.Login();
        }
    }
}
