using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class ExamService : IExamService
{
    public ExamPackage CreateExam(ExamPackage examPackage)
    {
        return new ExamPackage();
    }

    public Exam GetExamById(int idExam)
    {
        return new Exam();
    }

    public List<Exam> GetExamList()
    {
        throw new NotImplementedException();
    }

    public List<Exam> GetExamListByReviewer(int idReviewer)
    {
        return new List<Exam>();
    }

    public void SubmitExam(List<CandidateAnswer> candidateAnswerList)
    {

    }
}
