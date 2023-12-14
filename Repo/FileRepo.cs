using Bts.Helper;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class FileRepo : IFileRepo
{
    readonly DatabaseHelper _dbHelper;

    public FileRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public BTSFile CreateNewFile(BTSFile file)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_m_file (file_content, file_extension, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@file_content, @file_extension, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@file_content", file.FileContent);
        sqlCommand.Parameters.AddWithValue("@file_extension", file.FileExtension);
        sqlCommand.Parameters.AddWithValue("@created_by", file.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", file.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", file.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", file.IsActive);

        conn.Open();
        var newFileId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        file.Id = newFileId;
        return file;
    }
}
