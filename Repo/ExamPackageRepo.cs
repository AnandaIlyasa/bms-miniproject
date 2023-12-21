using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using Microsoft.EntityFrameworkCore;

namespace Bts.Repo;

internal class ExamPackageRepo : IExamPackageRepo
{
    public ExamPackage CreateNewExamPackage(ExamPackage examPackage, DBContextConfig context)
    {
        context.ExamPackages.Add(examPackage);
        context.SaveChanges();
        return examPackage;
    }

    public ExamPackage GetExamPackageByExam(int examId, DBContextConfig context)
    {
        var examPackage = context.ExamPackages
                .Where(ep => ep.ExamId == examId)
                .Include(ep => ep.Package)
                .GroupBy(ep => ep.ExamId)
                .Select(ep => ep.First())
                .First();
        return examPackage;
    }

    public int UpdateExamPackage(ExamPackage examPackage, DBContextConfig context)
    {
        var foundExamPackage = context.ExamPackages
            .Where(ep => ep.ExamId == examPackage.ExamId)
            .First();

        foundExamPackage.IsSubmitted = examPackage.IsSubmitted;
        foundExamPackage.ReviewerScore = examPackage.ReviewerScore;
        foundExamPackage.ExamStartDateTime = examPackage.ExamStartDateTime;
        foundExamPackage.ReviewerNotes = examPackage.ReviewerNotes;
        foundExamPackage.UpdatedBy = examPackage.UpdatedBy;

        return context.SaveChanges();
    }
}
