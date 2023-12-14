using Bts.Model;

namespace Bts.IRepo;

internal interface ICandidateAnswerRepo
{
    CandidateAnswer CreateNewCandidateAnswer(CandidateAnswer candidateAnswer);
}
