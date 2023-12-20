namespace Bts.Config;

using Bts.IRepo;
using Bts.Repo;
using Bts.Service;
using Bts.IService;
using Bts.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class DIConfig
{
    public static IHost Init()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddSingleton<IUserRepo, UserRepo>();
        builder.Services.AddSingleton<IRoleRepo, RoleRepo>();
        builder.Services.AddSingleton<IExamRepo, ExamRepo>();
        builder.Services.AddSingleton<IPackageRepo, PackageRepo>();
        builder.Services.AddSingleton<IExamPackageRepo, ExamPackageRepo>();
        builder.Services.AddSingleton<IQuestionRepo, QuestionRepo>();
        builder.Services.AddSingleton<IFileRepo, FileRepo>();
        builder.Services.AddSingleton<IMultipleChoiceOptionRepo, MultipleChoiceOptionRepo>();
        builder.Services.AddSingleton<IDocumentTypeRepo, DocumentTypeRepo>();
        builder.Services.AddSingleton<ICandidateDocumentRepo, CandidateDocumentRepo>();
        builder.Services.AddSingleton<ICandidateAnswerRepo, CandidateAnswerRepo>();

        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IExamService, ExamService>();
        builder.Services.AddSingleton<IPackageService, PackageService>();
        builder.Services.AddSingleton<IQuestionService, QuestionService>();
        builder.Services.AddSingleton<IDocumentService, DocumentService>();

        builder.Services.AddSingleton<SuperAdminView>();
        builder.Services.AddSingleton<HRView>();
        builder.Services.AddSingleton<ReviewerView>();
        builder.Services.AddSingleton<CandidateView>();
        builder.Services.AddSingleton<AuthView>();

        return builder.Build();
    }
}
