namespace Bts.IRepo;

using Bts.Model;

internal interface IExamRepo
{
    List<Exam> GetExamList();
    Exam GetExamById(int examId);
    Exam GetExamByCandidate(int candidateId);
    Exam CreateNewExam(Exam exam);
}
