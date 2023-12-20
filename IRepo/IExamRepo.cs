namespace Bts.IRepo;

using Bts.Config;
using Bts.Model;

internal interface IExamRepo
{
    List<Exam> GetExamList(DBContextConfig context);
    Exam GetExamById(int examId, DBContextConfig context);
    Exam? GetExamByCandidate(int candidateId, DBContextConfig context);
    List<Exam> GetExamListByReviewer(int reviewerId, DBContextConfig context);
    Exam CreateNewExam(Exam exam, DBContextConfig context);
}
