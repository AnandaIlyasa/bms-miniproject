namespace Bts;

using Bts.Helper;
using Bts.Repo;
using Bts.Service;
using Bts.View;
using System.Data.SqlClient;

internal class Program
{
    static void Main()
    {
        var dbHelper = new DatabaseHelper();

        var userRepo = new UserRepo(dbHelper);
        var roleRepo = new RoleRepo(dbHelper);
        var examRepo = new ExamRepo(dbHelper);

        var userService = new UserService(userRepo, roleRepo);
        var examService = new ExamService(examRepo);

        var superadminView = new SuperAdminView(userService, examService);
        var hrView = new HRView();
        var reviewerView = new ReviewerView();
        var candidateView = new CandidateView();
        var authView = new AuthView(userService, superadminView, hrView, reviewerView, candidateView);

        authView.Login();
    }
}
