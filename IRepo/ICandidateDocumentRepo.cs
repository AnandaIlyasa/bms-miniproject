using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface ICandidateDocumentRepo
{
    CandidateDocument CreateNewCandidateDocument(CandidateDocument candidateDocument, DBContextConfig context);
    List<CandidateDocument> GetCandidateDocumentList(int candidateId, DBContextConfig context);
}
