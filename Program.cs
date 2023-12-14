namespace Bts;

using Bts.Helper;
using Bts.Repo;
using Bts.Service;
using Bts.View;

internal class Program
{
    static void Main()
    {
        var dbHelper = new DatabaseHelper();

        var userRepo = new UserRepo(dbHelper);
        var roleRepo = new RoleRepo(dbHelper);
        var examRepo = new ExamRepo(dbHelper);
        var packageRepo = new PackageRepo(dbHelper);
        var examPackageRepo = new ExamPackageRepo(dbHelper);
        var questionRepo = new QuestionRepo(dbHelper);
        var fileRepo = new FileRepo(dbHelper);
        var optionRepo = new MultipleChoiceOptionRepo(dbHelper);
        var documentTypeRepo = new DocumentTypeRepo(dbHelper);
        var candidateDocumentRepo = new CandidateDocumentRepo(dbHelper);

        var userService = new UserService(userRepo, roleRepo);
        var examService = new ExamService(examRepo, examPackageRepo);
        var packageService = new PackageService(packageRepo);
        var questionService = new QuestionService(questionRepo, fileRepo, optionRepo);
        var documentService = new DocumentService(documentTypeRepo, fileRepo, candidateDocumentRepo);

        var superadminView = new SuperAdminView(userService, examService);
        var hrView = new HRView(userService, packageService, examService);
        var reviewerView = new ReviewerView(packageService, questionService);
        var candidateView = new CandidateView(documentService);
        var authView = new AuthView(userService, superadminView, hrView, reviewerView, candidateView);

        authView.Login();
    }
}
