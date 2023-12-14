using Bts.Helper;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class MultipleChoiceOptionRepo : IMultipleChoiceOptionRepo
{
    readonly DatabaseHelper _dbHelper;

    public MultipleChoiceOptionRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public MultipleChoiceOption CreateNewOption(MultipleChoiceOption option)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_m_multiple_choice_option (option_char, option_text, is_correct, question_id, option_image_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@option_char, @option_text, @is_correct, @question_id, @option_image_id, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@option_char", option.OptionChar);
        sqlCommand.Parameters.AddWithValue("@option_text", option.OptionText == null ? DBNull.Value : option.OptionText);
        sqlCommand.Parameters.AddWithValue("@is_correct", option.IsCorrect);
        sqlCommand.Parameters.AddWithValue("@question_id", option.Question.Id);
        sqlCommand.Parameters.AddWithValue("@option_image_id", option.OptionImage == null ? DBNull.Value : option.OptionImage.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", option.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", option.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", option.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", option.IsActive);

        conn.Open();
        var newOptionId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        option.Id = newOptionId;
        return option;
    }
}
