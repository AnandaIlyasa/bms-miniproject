using Bts.Helper;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class CandidateAnswerRepo : ICandidateAnswerRepo
{
    readonly DatabaseHelper _dbHelper;

    public CandidateAnswerRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public CandidateAnswer CreateNewCandidateAnswer(CandidateAnswer candidateAnswer)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_candidate_answer (answer_content, exam_package_id, choice_option_id, question_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@answer_content, @exam_package_id, @choice_option_id, @question_id, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@answer_content", candidateAnswer.AnswerContent == null ? DBNull.Value : candidateAnswer.AnswerContent);
        sqlCommand.Parameters.AddWithValue("@exam_package_id", candidateAnswer.ExamPackage.Id);
        sqlCommand.Parameters.AddWithValue("@choice_option_id", candidateAnswer.ChoiceOption == null ? DBNull.Value : candidateAnswer.ChoiceOption.Id);
        sqlCommand.Parameters.AddWithValue("@question_id", candidateAnswer.Question.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", candidateAnswer.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", candidateAnswer.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", candidateAnswer.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", candidateAnswer.IsActive);

        conn.Open();
        var newCandidateAnswerId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        candidateAnswer.Id = newCandidateAnswerId;
        return candidateAnswer;
    }
}
