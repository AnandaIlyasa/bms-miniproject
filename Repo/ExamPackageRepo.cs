﻿using Bts.Helper;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class ExamPackageRepo : IExamPackageRepo
{
    readonly DatabaseHelper _dbHelper;

    public ExamPackageRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public ExamPackage CreateNewExamPackage(ExamPackage examPackage)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_exam_package (package_id, exam_id, duration, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@package_id, @exam_id, @duration, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@package_id", examPackage.Package.Id);
        sqlCommand.Parameters.AddWithValue("@exam_id", examPackage.Exam.Id);
        sqlCommand.Parameters.AddWithValue("@duration", examPackage.Duration);
        sqlCommand.Parameters.AddWithValue("@created_by", examPackage.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", examPackage.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", examPackage.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", examPackage.IsActive);

        conn.Open();
        var newExamPackageId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        examPackage.Id = newExamPackageId;
        return examPackage;
    }
}