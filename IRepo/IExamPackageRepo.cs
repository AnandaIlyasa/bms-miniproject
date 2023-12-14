using Bts.Model;

namespace Bts.IRepo;

internal interface IExamPackageRepo
{
    ExamPackage CreateNewExamPackage(ExamPackage examPackage);
}
