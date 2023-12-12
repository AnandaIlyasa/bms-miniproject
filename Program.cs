namespace Bts;

using Bts.View;

internal class Program
{
    static void Main()
    {
        var authView = new AuthView(new SuperAdminView(), new CandidateView(), new HRView(), new ReviewerView());
        authView.Login();
    }
}
