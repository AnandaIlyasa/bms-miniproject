using Bts.Model;

namespace Bts.IService;

internal interface IExamService
{
    List<Exam> GetExamListByReviewer(int reviewerId);
    List<Exam> GetExamList();
    Exam GetExamById(int examId);
    Exam GetExamByCandidate(int candidateId);
    ExamPackage CreateExam(ExamPackage examPackage);
    void SubmitExam(List<CandidateAnswer> candidateAnswerList, ExamPackage examPackage);
}
