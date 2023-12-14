namespace Bts.Repo;

using Bts.Helper;
using Bts.IRepo;
using Bts.Model;
using System.Collections.Generic;

internal class ExamRepo : IExamRepo
{
    readonly DatabaseHelper _dbHelper;

    public ExamRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public Exam CreateNewExam(Exam exam)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_exam (candidate_id, reviewer_id, login_start, login_end, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@candidate_id, @reviewer_id, @login_start, @login_end, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@candidate_id", exam.Candidate.Id);
        sqlCommand.Parameters.AddWithValue("@reviewer_id", exam.Reviewer.Id);
        sqlCommand.Parameters.AddWithValue("@login_start", exam.LoginStart);
        sqlCommand.Parameters.AddWithValue("@login_end", exam.LoginEnd);
        sqlCommand.Parameters.AddWithValue("@created_by", exam.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", exam.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", exam.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", exam.IsActive);

        conn.Open();
        var newExamId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        exam.Id = newExamId;
        return exam;
    }

    public Exam GetExamByCandidate(int candidateId)
    {
        var sqlQuery =
            "SELECT " +
                "e.id, " +
                "can.full_name AS candidate_name, " +
                "p.package_name, " +
                "ep.is_submitted, " +
                "q.question, " +
                "qi.file_content AS question_image, " +
                "qi.file_extension AS question_image_extension, " +
                "mco.option_char, " +
                "oi.file_content AS option_image, " +
                "oi.file_extension AS option_image_extension, " +
                "mco.option_text " +
            "FROM " +
                "t_r_exam e " +
            "JOIN " +
                "t_m_user can ON e.candidate_id = can.id " +
            "JOIN " +
                "t_r_exam_package ep ON e.id = ep.exam_id " +
            "JOIN " +
                "t_m_package p ON ep.package_id = p.id " +
            "JOIN " +
                "t_m_question q ON p.id = q.package_id " +
            "JOIN " +
                "t_m_multiple_choice_option mco ON q.id = mco.question_id " +
            "LEFT JOIN " +
                "t_m_file qi ON q.image_id = qi.id " +
            "LEFT JOIN " +
                "t_m_file oi ON mco.option_image_id = oi.id " +
            "WHERE " +
                "can.id = @candidate_id";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@candidate_id", candidateId);
        var reader = sqlCommand.ExecuteReader();
        Exam exam = new Exam() { CandidateAnswerList = new List<CandidateAnswer>() };
        var number = 1;
        while (reader.Read())
        {
            if (number == 1)
            {
                exam.Id = (int)reader["id"];
                exam.CreatedAt = (DateTime)reader["created_at"];
                exam.Candidate = new User() { FullName = (string)reader["candidate_name"] };
                exam.Reviewer = new User() { FullName = (string)reader["reviewer_name"] };
                exam.Package = new Package() { PackageName = (string)reader["package_name"] };
                exam.AcceptanceStatus = new AcceptanceStatus() { StatusName = reader["status_name"] is string ? (string)reader["status_name"] : "" };
                exam.ExamPackage = new ExamPackage()
                {
                    IsSubmitted = reader["is_submitted"] as bool?,
                    ReviewerScore = reader["reviewer_score"] as float?,
                    ReviewerNotes = reader["reviewer_notes"] is string ? (string)reader["reviewer_notes"] : null,
                };
            }

            var questionImage = reader["question_image"] is string ? new BTSFile()
            {
                FileContent = reader["question_image"] is string ? (string)reader["question_image"] : "",
                FileExtension = reader["question_image_extension"] is string ? (string)reader["question_image_extension"] : "",
            } : null;
            var optionImage = reader["option_image"] is string ? new BTSFile()
            {
                FileContent = reader["option_image"] is string ? (string)reader["option_image"] : "",
                FileExtension = reader["option_image_extension"] is string ? (string)reader["option_image_extension"] : "",
            } : null;

            var optText = reader["option_text"] is string ? (string?)reader["option_text"] : null;
            var questionText = reader["question"] is string ? (string?)reader["question"] : null;
            var optChar = reader["option_char"] is string ? (string)reader["option_char"] : "";

            var choiceOpt = new MultipleChoiceOption()
            {
                OptionChar = optChar,
                OptionText = optText,
                OptionImage = optionImage
            };

            var question = new Question()
            {
                Image = questionImage,
                QuestionContent = questionText,
            };

            var ansContent = reader["answer_content"] is string ? (string)reader["answer_content"] : null;

            var candidateAnswer = new CandidateAnswer()
            {
                AnswerContent = ansContent,
                ChoiceOption = choiceOpt,
                Question = question,
            };
            exam.CandidateAnswerList.Add(candidateAnswer);

            number++;
        }

        conn.Close();

        return exam;
    }

    public Exam GetExamById(int examId)
    {
        var sqlQuery =
            "SELECT " +
                "e.id, " +
                "can.full_name AS candidate_name, " +
                "rev.full_name AS reviewer_name, " +
                "p.package_name, " +
                "e.created_at, " +
                "ep.is_submitted, " +
                "ep.reviewer_score, " +
                "ep.reviewer_notes, " +
                "q.question, " +
                "qi.file_content AS question_image, " +
                "qi.file_extension AS question_image_extension, " +
                "ca.answer_content, " +
                "mco.option_char, " +
                "oi.file_content AS option_image, " +
                "oi.file_extension AS option_image_extension, " +
                "mco.option_text, " +
                "acs.status_name " +
            "FROM " +
                "t_r_exam e " +
            "LEFT JOIN " +
                "t_m_acceptance_status acs ON e.acceptance_status_id = acs.id " +
            "JOIN " +
                "t_m_user can ON e.candidate_id = can.id " +
            "JOIN " +
                "t_m_user rev ON e.reviewer_id = rev.id " +
            "JOIN " +
                "t_r_exam_package ep ON e.id = ep.exam_id " +
            "JOIN " +
                "t_m_package p ON ep.package_id = p.id " +
            "LEFT JOIN " +
                "t_r_candidate_answer ca ON ep.id = ca.exam_package_id " +
            "LEFT JOIN " +
                "t_m_question q ON ca.question_id = q.id " +
            "LEFT JOIN " +
                "t_m_multiple_choice_option mco ON ca.choice_option_id = mco.id " +
            "LEFT JOIN " +
                "t_m_file qi ON q.image_id = qi.id " +
            "LEFT JOIN " +
                "t_m_file oi ON mco.option_image_id = oi.id " +
             "WHERE " +
                "e.id = @exam_id";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@exam_id", examId);
        var reader = sqlCommand.ExecuteReader();
        Exam exam = new Exam() { CandidateAnswerList = new List<CandidateAnswer>() };
        var number = 1;
        while (reader.Read())
        {
            if (number == 1)
            {
                exam.Id = (int)reader["id"];
                exam.CreatedAt = (DateTime)reader["created_at"];
                exam.Candidate = new User() { FullName = (string)reader["candidate_name"] };
                exam.Reviewer = new User() { FullName = (string)reader["reviewer_name"] };
                exam.Package = new Package() { PackageName = (string)reader["package_name"] };
                exam.AcceptanceStatus = new AcceptanceStatus() { StatusName = reader["status_name"] is string ? (string)reader["status_name"] : "" };
                exam.ExamPackage = new ExamPackage()
                {
                    IsSubmitted = reader["is_submitted"] as bool?,
                    ReviewerScore = reader["reviewer_score"] as float?,
                    ReviewerNotes = reader["reviewer_notes"] is string ? (string)reader["reviewer_notes"] : null,
                };
            }

            var questionImage = reader["question_image"] is string ? new BTSFile()
            {
                FileContent = reader["question_image"] is string ? (string)reader["question_image"] : "",
                FileExtension = reader["question_image_extension"] is string ? (string)reader["question_image_extension"] : "",
            } : null;
            var optionImage = reader["option_image"] is string ? new BTSFile()
            {
                FileContent = reader["option_image"] is string ? (string)reader["option_image"] : "",
                FileExtension = reader["option_image_extension"] is string ? (string)reader["option_image_extension"] : "",
            } : null;

            var optText = reader["option_text"] is string ? (string?)reader["option_text"] : null;
            var questionText = reader["question"] is string ? (string?)reader["question"] : null;
            var optChar = reader["option_char"] is string ? (string)reader["option_char"] : "";

            var choiceOpt = new MultipleChoiceOption()
            {
                OptionChar = optChar,
                OptionText = optText,
                OptionImage = optionImage
            };

            var question = new Question()
            {
                Image = questionImage,
                QuestionContent = questionText,
            };

            var ansContent = reader["answer_content"] is string ? (string)reader["answer_content"] : null;

            var candidateAnswer = new CandidateAnswer()
            {
                AnswerContent = ansContent,
                ChoiceOption = choiceOpt,
                Question = question,
            };
            exam.CandidateAnswerList.Add(candidateAnswer);

            number++;
        }

        conn.Close();

        return exam;
    }

    public List<Exam> GetExamList()
    {
        const string sqlQuery =
            "SELECT " +
                "e.id," +
                "can.full_name," +
                "p.package_name," +
                "e.created_at," +
                "ep.is_submitted," +
                "acs.status_name " +
            "FROM " +
                "t_r_exam e " +
            "JOIN " +
                "t_m_user can ON e.candidate_id = can.id " +
            "LEFT JOIN " +
                "t_m_acceptance_status acs ON e.acceptance_status_id = acs.id " +
            "JOIN " +
                "t_r_exam_package ep ON e.id = ep.exam_id " +
            "JOIN " +
                "t_m_package p ON ep.package_id = p.id";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        var reader = sqlCommand.ExecuteReader();
        List<Exam> examList = new List<Exam>();
        while (reader.Read())
        {
            var exam = new Exam()
            {
                Id = (int)reader["id"],
                CreatedAt = (DateTime)reader["created_at"],
                Candidate = new User()
                {
                    FullName = (string)reader["full_name"]
                },
                Package = new Package()
                {
                    PackageName = (string)reader["package_name"]
                },
                AcceptanceStatus = new AcceptanceStatus()
                {
                    StatusName = reader["status_name"] is string ? (string)reader["status_name"] : ""
                },
                ExamPackage = new ExamPackage()
                {
                    IsSubmitted = reader["is_submitted"] as bool?
                }
            };

            examList.Add(exam);
        }

        conn.Close();

        return examList;
    }
}
