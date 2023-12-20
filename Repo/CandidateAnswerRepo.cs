using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using Microsoft.EntityFrameworkCore;

namespace Bts.Repo;

internal class CandidateAnswerRepo : ICandidateAnswerRepo
{
    public CandidateAnswer CreateNewCandidateAnswer(CandidateAnswer candidateAnswer, DBContextConfig context)
    {
        context.CandidateAnswers.Add(candidateAnswer);
        context.SaveChanges();
        return candidateAnswer;
    }

    public List<CandidateAnswer> GetCandidateAnswerListByExamPackage(int examPackageId, DBContextConfig context)
    {
        var candidateAnswerList = context.CandidateAnswers
                .Where(ca => ca.ExamPackageId == examPackageId)
                .Include(ca => ca.Question)
                .ThenInclude(q => q.Image)
                .Include(ca => ca.ChoiceOption)
                .ThenInclude(o => o == null ? null : o.OptionImage)
                .ToList();
        return candidateAnswerList;
    }
}
