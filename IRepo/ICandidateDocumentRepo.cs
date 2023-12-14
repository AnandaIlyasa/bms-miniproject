using Bts.Model;

namespace Bts.IRepo;

internal interface ICandidateDocumentRepo
{
    CandidateDocument CreateNewCandidateDocument(CandidateDocument candidateDocument);
    List<CandidateDocument> GetCandidateDocumentList(int candidateId);
}
