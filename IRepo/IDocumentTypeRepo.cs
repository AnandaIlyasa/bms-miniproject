using Bts.Model;

namespace Bts.IRepo;

internal interface IDocumentTypeRepo
{
    List<DocumentType> GetDocumentTypeList();
}
