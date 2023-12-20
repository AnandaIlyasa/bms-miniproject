using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using Microsoft.EntityFrameworkCore;

namespace Bts.Repo;

internal class CandidateDocumentRepo : ICandidateDocumentRepo
{
    public CandidateDocument CreateNewCandidateDocument(CandidateDocument candidateDocument, DBContextConfig context)
    {
        context.CandidateDocuments.Add(candidateDocument);
        context.SaveChanges();
        return candidateDocument;
    }

    public List<CandidateDocument> GetCandidateDocumentList(int candidateId, DBContextConfig context)
    {
        var candidateDocumentList = context.CandidateDocuments
                        .Where(cd => cd.CandidateId == candidateId)
                        .Include(cd => cd.File)
                        .Include(cd => cd.DocumentType)
                        .ToList();

        return candidateDocumentList;
    }
}
