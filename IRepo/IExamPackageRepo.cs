using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface IExamPackageRepo
{
    ExamPackage GetExamPackageByExam(int examId, DBContextConfig context);
    ExamPackage CreateNewExamPackage(ExamPackage examPackage, DBContextConfig context);
    int UpdateExamPackage(ExamPackage examPackage, DBContextConfig context);
    int UpdateReviewerScoreAndNotesOnExamPackage(ExamPackage examPackage, DBContextConfig context);
}
