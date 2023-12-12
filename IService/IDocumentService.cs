using Bts.Model;

namespace Bts.IService;

internal interface IDocumentService
{
    void UploadCandidateDocument(List<CandidateDocument> candidateDocumentList);
}
