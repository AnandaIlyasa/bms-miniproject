using Bts.Model;

namespace Bts.IService;

internal interface IExamService
{
    List<Exam> GetExamListByReviewer(int reviewerId);
    List<Exam> GetExamList();
    Exam GetExamById(int examId);
    ExamPackage CreateExam(ExamPackage examPackage);
    void SubmitExam(List<CandidateAnswer> candidateAnswerList);
}
