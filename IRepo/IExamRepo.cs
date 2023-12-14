namespace Bts.IRepo;

using Bts.Model;

internal interface IExamRepo
{
    List<Exam> GetExamList();
    Exam GetExamById(int examId);
    Exam? GetExamByCandidate(int candidateId);
    List<Exam> GetExamListByReviewer(int reviewerId);
    Exam CreateNewExam(Exam exam);
}
