using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class ExamService : IExamService
{
    IExamRepo _examRepo;

    public ExamService(IExamRepo examRepo)
    {
        _examRepo = examRepo;
    }

    public ExamPackage CreateExam(ExamPackage examPackage)
    {
        return new ExamPackage();
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
