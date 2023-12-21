using Bts.Config;
using Bts.Constant;
using Bts.Helper;
using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class DocumentService : IDocumentService
{
    readonly IDocumentTypeRepo _documentTypeRepo;
    readonly IFileRepo _fileRepo;
    readonly ICandidateDocumentRepo _candidateDocumentRepo;
    readonly SessionHelper _sessionHelper;

    public DocumentService(IDocumentTypeRepo documentTypeRepo, IFileRepo fileRepo, ICandidateDocumentRepo candidateDocumentRepo, SessionHelper sessionHelper)
    {
        _documentTypeRepo = documentTypeRepo;
        _fileRepo = fileRepo;
        _candidateDocumentRepo = candidateDocumentRepo;
        _sessionHelper = sessionHelper;
    }

    public List<CandidateDocument> GetCandidateCVAndTranscript(int candidateId)
    {
        var candidateDocumentList = new List<CandidateDocument>();
        using (var context = new DBContextConfig())
        {
            candidateDocumentList = _candidateDocumentRepo.GetCandidateDocumentList(candidateId, context);
        }
        candidateDocumentList = candidateDocumentList
            .Where(cd => cd.DocumentType.Code == DocumentTypeCode.CurriculumVitae || cd.DocumentType.Code == DocumentTypeCode.Transcript)
            .ToList();
        return candidateDocumentList;
    }

    public CandidateDocumentHelper GetCandidateDocumentList(int candidateId)
    {
        var candidateDocumentHelper = new CandidateDocumentHelper();
        using (var context = new DBContextConfig())
        {
            var candidateDocumentList = _candidateDocumentRepo.GetCandidateDocumentList(candidateId, context);
            var documentTypeList = _documentTypeRepo.GetDocumentTypeList(context);
            var allDocumentIsUploaded = true;
            foreach (var documentType in documentTypeList)
            {
                var documentIsNotUploaded = candidateDocumentList.Exists(cd => cd.DocumentType.TypeName == documentType.TypeName) == false;
                if (documentIsNotUploaded)
                {
                    allDocumentIsUploaded = false;
                    break;
                }
            }
            candidateDocumentHelper.CandidateDocumentList = candidateDocumentList;
            candidateDocumentHelper.AllDocumentIsUploaded = allDocumentIsUploaded;
        }
        return candidateDocumentHelper;
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
                candidateDocument.File.CreatedBy = _sessionHelper.UserId;
                var uploadedFile = _fileRepo.CreateNewFile(candidateDocument.File, context);
                candidateDocument.FileId = uploadedFile.Id;
                candidateDocument.DocumentType = null;
                candidateDocument.CreatedBy = _sessionHelper.UserId;
                _candidateDocumentRepo.CreateNewCandidateDocument(candidateDocument, context);
            }

            trx.Commit();
        }
    }
}
