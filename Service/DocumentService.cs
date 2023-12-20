using Bts.Config;
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
        var candidateDocumentList = new List<CandidateDocument>();
        using (var context = new DBContextConfig())
        {
            candidateDocumentList = _candidateDocumentRepo.GetCandidateDocumentList(candidateId, context);
        }
        return candidateDocumentList;
    }

    public List<DocumentType> GetDocumentTypeList()
    {
        var documentTypeList = new List<DocumentType>();
        using (var context = new DBContextConfig())
        {
            documentTypeList = _documentTypeRepo.GetDocumentTypeList(context);
        }
        return documentTypeList;
    }

    public void UploadCandidateDocument(List<CandidateDocument> candidateDocumentList)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            foreach (var candidateDocument in candidateDocumentList)
            {
                var uploadedFile = _fileRepo.CreateNewFile(candidateDocument.File, context);
                candidateDocument.FileId = uploadedFile.Id;
                candidateDocument.DocumentType = null;
                _candidateDocumentRepo.CreateNewCandidateDocument(candidateDocument, context);
            }

            trx.Commit();
        }
    }
}
