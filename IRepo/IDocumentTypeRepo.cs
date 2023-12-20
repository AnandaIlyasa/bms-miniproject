using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface IDocumentTypeRepo
{
    List<DocumentType> GetDocumentTypeList(DBContextConfig context);
}
