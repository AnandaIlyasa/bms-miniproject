using Bts.Constant;
using Bts.Helper;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class CandidateDocumentRepo : ICandidateDocumentRepo
{
    readonly DatabaseHelper _dbHelper;

    public CandidateDocumentRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public CandidateDocument CreateNewCandidateDocument(CandidateDocument candidateDocument)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_candidate_document (candidate_id, file_id, document_type_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@candidate_id, @file_id, @document_type_id, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@candidate_id", candidateDocument.Candidate.Id);
        sqlCommand.Parameters.AddWithValue("@file_id", candidateDocument.File.Id);
        sqlCommand.Parameters.AddWithValue("@document_type_id", candidateDocument.DocumentType.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", candidateDocument.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", candidateDocument.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", candidateDocument.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", candidateDocument.IsActive);

        conn.Open();
        var newCandidateDocumentId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        candidateDocument.Id = newCandidateDocumentId;
        return candidateDocument;
    }

    public List<CandidateDocument> GetCandidateDocumentList(int candidateId)
    {
        const string sqlQuery =
            "SELECT " +
                "cd.id, " +
                "f.file_content, " +
                "f.file_extension, " +
                "dt.type_name " +
            "FROM " +
                "t_r_candidate_document cd " +
            "JOIN " +
                "t_m_user u ON cd.candidate_id = u.id " +
            "JOIN " +
                "t_m_document_type dt ON cd.document_type_id = dt.id " +
            "JOIN " +
                "t_m_file f ON cd.file_id = f.id " +
            "WHERE " +
                "cd.candidate_id = @candidate_id";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@candidate_id", candidateId);
        var reader = sqlCommand.ExecuteReader();
        List<CandidateDocument> candidateDocumentList = new List<CandidateDocument>();
        while (reader.Read())
        {
            var file = new BTSFile()
            {
                FileContent = (string)reader["file_content"],
                FileExtension = (string)reader["file_extension"],
            };

            var candidate = new User() { Id = candidateId };

            var documentType = new DocumentType() { TypeName = (string)reader["type_name"] };

            var candidateDocument = new CandidateDocument()
            {
                Id = (int)reader["id"],
                DocumentType = documentType,
                File = file,
                Candidate = candidate,
            };
            candidateDocumentList.Add(candidateDocument);
        }

        conn.Close();

        return candidateDocumentList;
    }
}
