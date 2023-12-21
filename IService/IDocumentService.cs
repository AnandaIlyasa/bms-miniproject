using Bts.Model;
using Bts.Helper;

namespace Bts.IService;

internal interface IDocumentService
{
    void UploadCandidateDocument(List<CandidateDocument> candidateDocumentList);
    List<DocumentType> GetDocumentTypeList();
    CandidateDocumentHelper GetCandidateDocumentList(int candidateId);
    List<CandidateDocument> GetCandidateCVAndTranscript(int candidateId);
}
