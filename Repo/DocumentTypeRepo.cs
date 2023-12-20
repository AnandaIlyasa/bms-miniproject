using Bts.Config;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class DocumentTypeRepo : IDocumentTypeRepo
{
    public List<DocumentType> GetDocumentTypeList(DBContextConfig context)
    {
        var documentTypeList = context.DocumentTypes.ToList();
        return documentTypeList;
    }
}
