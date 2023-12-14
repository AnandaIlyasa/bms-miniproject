using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class ExamService : IExamService
{
    IExamRepo _examRepo;
    IExamPackageRepo _examPackageRepo;

    public ExamService(IExamRepo examRepo, IExamPackageRepo examPackageRepo)
    {
        _examRepo = examRepo;
        _examPackageRepo = examPackageRepo;
    }

    public ExamPackage CreateExam(ExamPackage examPackage)
    {
        var newExam = _examRepo.CreateNewExam(examPackage.Exam);
        examPackage.Exam.Id = newExam.Id;
        var newExamPackage = _examPackageRepo.CreateNewExamPackage(examPackage);
        return newExamPackage;
    }

    public Exam GetExamById(int examId)
    {
        return _examRepo.GetExamById(examId);
    }

    public List<Exam> GetExamList()
    {
        var repoList = _examRepo.GetExamList();
        return repoList;
    }

    public List<Exam> GetExamListByReviewer(int idReviewer)
    {
        return new List<Exam>();
    }

    public void SubmitExam(List<CandidateAnswer> candidateAnswerList)
    {

    }
}
