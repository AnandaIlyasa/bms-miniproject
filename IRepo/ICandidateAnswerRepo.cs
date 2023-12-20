using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface ICandidateAnswerRepo
{
    CandidateAnswer CreateNewCandidateAnswer(CandidateAnswer candidateAnswer, DBContextConfig context);
    List<CandidateAnswer> GetCandidateAnswerListByExamPackage(int examPackageId, DBContextConfig context);
}
