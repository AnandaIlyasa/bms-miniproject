using Bts.Config;
using Bts.Model;

namespace Bts.IService;

internal interface IExamService
{
    List<Exam> GetExamListByReviewer(int reviewerId);
    List<Exam> GetExamList();
    Exam GetExamById(int examId);
    Exam? GetExamByCandidate(int candidateId);
    ExamPackage CreateExam(ExamPackage examPackage);
    void StartExam(ExamPackage examPackage);
    void SubmitExam(List<CandidateAnswer> candidateAnswerList, ExamPackage examPackage);
    void InsertScoreNotesAcceptanceStatusOnExamPackage(ExamPackage examPackage, double essayScore);
    List<AcceptanceStatus> GetAcceptanceStatusList();
}
