namespace Bts;

using Bts.Config;
using Bts.View;
using Microsoft.Extensions.DependencyInjection;


//public class Item
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//}

internal class Program
{
    static void Main()
    {
        //var dbHelper = new DatabaseHelper();

        //var userRepo = new UserRepo(dbHelper);
        //var roleRepo = new RoleRepo(dbHelper);
        //var examRepo = new ExamRepo(dbHelper);
        //var packageRepo = new PackageRepo(dbHelper);
        //var examPackageRepo = new ExamPackageRepo(dbHelper);
        //var questionRepo = new QuestionRepo(dbHelper);
        //var fileRepo = new FileRepo(dbHelper);
        //var optionRepo = new MultipleChoiceOptionRepo(dbHelper);
        //var documentTypeRepo = new DocumentTypeRepo(dbHelper);
        //var candidateDocumentRepo = new CandidateDocumentRepo(dbHelper);
        //var candidateAnswerRepo = new CandidateAnswerRepo(dbHelper);

        //var userService = new UserService(userRepo, roleRepo);
        //var examService = new ExamService(examRepo, examPackageRepo, candidateAnswerRepo);
        //var packageService = new PackageService(packageRepo);
        //var questionService = new QuestionService(questionRepo, fileRepo, optionRepo);
        //var documentService = new DocumentService(documentTypeRepo, fileRepo, candidateDocumentRepo);

        //var superadminView = new SuperAdminView(userService, examService);
        //var hrView = new HRView(userService, packageService, examService);
        //var reviewerView = new ReviewerView(packageService, questionService, examService);
        //var candidateView = new CandidateView(documentService, examService);
        //var authView = new AuthView(userService, superadminView, hrView, reviewerView, candidateView);

        var host = DIConfig.Init();
        var authView = host.Services.GetService<AuthView>();
        authView.Login();



        //var list = new List<Item>()
        //{
        //    new Item(){ Id = 1, Name = "ananda"},
        //    new Item(){ Id = 1, Name = "ilyasa"},
        //    new Item(){ Id = 2, Name = "putra"},
        //};

        //list = list.GroupBy(p => p.Id).Select(p => p.First()).ToList();

        //foreach (var item in list)
        //{
        //    Console.WriteLine(item.Name);
        //}
    }
}
