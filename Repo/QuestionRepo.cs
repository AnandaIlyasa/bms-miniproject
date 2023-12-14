using Bts.Constant;
using Bts.Helper;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class QuestionRepo : IQuestionRepo
{
    readonly DatabaseHelper _dbHelper;

    public QuestionRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public Question CreateNewQuestion(Question question)
    {
        const string sqlQuery =
            "INSERT INTO " +
            "t_m_question (question, package_id, image_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
            "(@question, @package_id, @image_id, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@question", question.QuestionContent == null ? DBNull.Value : question.QuestionContent);
        sqlCommand.Parameters.AddWithValue("@package_id", question.Package.Id);
        sqlCommand.Parameters.AddWithValue("@image_id", question.Image == null ? DBNull.Value : question.Image?.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", question.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", question.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", question.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", question.IsActive);

        conn.Open();
        var newQuestionId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        question.Id = newQuestionId;
        return question;
    }

    public List<Question> GetQuestionListByPackage(int packageId)
    {
        const string sqlQuery =
            "SELECT " +
                "q.id, " +
                "q.question, " +
                "q_f.file_content AS question_image, " +
                "q_f.file_extension AS question_image_extension, " +
                "mco.option_char, " +
                "mco.option_text, " +
                "mco.is_correct, " +
                "o_f.file_content AS option_image, " +
                "o_f.file_extension AS option_image_extension " +
            "FROM " +
                "t_m_question q " +
            "LEFT JOIN " +
                "t_m_file q_f ON q.image_id = q_f.id " +
            "LEFT JOIN " +
                "t_m_multiple_choice_option mco ON q.id = mco.question_id " +
            "LEFT JOIN " +
                "t_m_file o_f ON mco.option_image_id = o_f.id " +
            "WHERE " +
                "q.package_id = @package_id";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@package_id", packageId);
        var reader = sqlCommand.ExecuteReader();
        List<Question> questionList = new List<Question>();
        while (reader.Read())
        {
            MultipleChoiceOption? choiceOpt = null;
            if (reader["option_char"] is string)
            {
                var optText = reader["option_text"] is string ? (string?)reader["option_text"] : null;
                var optionImage = reader["option_image"] is string ? new BTSFile()
                {
                    FileContent = reader["option_image"] is string ? (string)reader["option_image"] : "",
                    FileExtension = reader["option_image_extension"] is string ? (string)reader["option_image_extension"] : "",
                } : null;
                var optChar = reader["option_char"] is string ? (string)reader["option_char"] : "";
                choiceOpt = new MultipleChoiceOption()
                {
                    OptionChar = optChar,
                    OptionText = optText,
                    OptionImage = optionImage
                };
            }

            var questionId = (int)reader["id"];
            var existingQuestion = questionList.Find(q => q.Id == questionId);
            if (existingQuestion != null && choiceOpt != null)
            {
                existingQuestion.OptionList.Add(choiceOpt);
            }
            else
            {
                var questionImage = reader["question_image"] is string ? new BTSFile()
                {
                    FileContent = reader["question_image"] is string ? (string)reader["question_image"] : "",
                    FileExtension = reader["question_image_extension"] is string ? (string)reader["question_image_extension"] : "",
                } : null;
                var questionText = reader["question"] is string ? (string?)reader["question"] : null;

                var optionList = new List<MultipleChoiceOption>();
                if (choiceOpt != null)
                {
                    optionList.Add(choiceOpt);
                }

                var question = new Question()
                {
                    Id = questionId,
                    Image = questionImage,
                    QuestionContent = questionText,
                };
                if (optionList.Count > 0)
                {
                    question.OptionList = optionList;
                }

                questionList.Add(question);
            }
        }

        conn.Close();

        return questionList;
    }
}
