using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class DocumentService : IDocumentService
{
    readonly IDocumentTypeRepo _documentTypeRepo;
    readonly IFileRepo _fileRepo;
    readonly ICandidateDocumentRepo _candidateDocumentRepo;

    public DocumentService(IDocumentTypeRepo documentTypeRepo, IFileRepo fileRepo, ICandidateDocumentRepo candidateDocumentRepo)
    {
        _documentTypeRepo = documentTypeRepo;
        _fileRepo = fileRepo;
        _candidateDocumentRepo = candidateDocumentRepo;
    }

    public List<CandidateDocument> GetCandidateDocumentList(int candidateId)
    {
        var candidateDocumentList = _candidateDocumentRepo.GetCandidateDocumentList(candidateId);
        return candidateDocumentList;
    }

    public List<DocumentType> GetDocumentTypeList()
    {
        var documentTypeList = _documentTypeRepo.GetDocumentTypeList();
        return documentTypeList;
    }

    public void UploadCandidateDocument(List<CandidateDocument> candidateDocumentList)
    {
        foreach (var candidateDocument in candidateDocumentList)
        {
            var uploadedFile = _fileRepo.CreateNewFile(candidateDocument.File);
            candidateDocument.File.Id = uploadedFile.Id;
            _candidateDocumentRepo.CreateNewCandidateDocument(candidateDocument);
        }
    }
}
