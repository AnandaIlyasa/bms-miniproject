using Bts.Constant;
using Bts.Helper;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class DocumentTypeRepo : IDocumentTypeRepo
{
    readonly DatabaseHelper _dbHelper;

    public DocumentTypeRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public List<DocumentType> GetDocumentTypeList()
    {
        const string sqlQuery =
            "SELECT " +
                "id, " +
                "code, " +
                "type_name " +
            "FROM " +
                "t_m_document_type";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        var reader = sqlCommand.ExecuteReader();
        List<DocumentType> documentTypeList = new List<DocumentType>();
        while (reader.Read())
        {
            var documentType = new DocumentType()
            {
                Id = (int)reader["id"],
                Code = (string)reader["code"],
                TypeName = (string)reader["type_name"],
            };
            documentTypeList.Add(documentType);
        }

        conn.Close();

        return documentTypeList;
    }
}
