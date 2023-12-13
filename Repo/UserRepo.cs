﻿using Bts.Helper;
using Bts.IRepo;
using Bts.Model;
using System.Data.SqlClient;

namespace Bts.Repo;

internal class UserRepo : IUserRepo
{
    readonly DatabaseHelper _dbHelper;

    public UserRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public User CreateNewUser(User user)
    {
        const string sqlQuery = "INSERT INTO " +
            "t_m_user(full_name, email, pass, role_id, created_by, created_at, ver, is_active) VALUES " +
            "(@full_name, @email, @pass, @role_id, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@full_name", user.FullName);
        sqlCommand.Parameters.AddWithValue("@email", user.Email);
        sqlCommand.Parameters.AddWithValue("@pass", user.Pass);
        sqlCommand.Parameters.AddWithValue("@role_id", user.Role.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", user.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", user.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", user.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", user.IsActive);

        conn.Open();
        var newUserId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        user.Id = newUserId;
        return user;
    }

    public User? GetUserByEmailAndPassword(string email, string password)
    {
        const string sql = "SELECT * FROM t_m_user u " +
                            "JOIN t_m_role r ON u.role_id = r.id " +
                            "WHERE email = @email AND pass LIKE @password";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sql;
        sqlCommand.Parameters.AddWithValue("@email", email);
        sqlCommand.Parameters.AddWithValue("@password", password);
        conn.Open();
        var reader = sqlCommand.ExecuteReader();
        User? user = null;
        if (reader.Read())
        {
            user = new User()
            {
                Id = (int)reader["id"],
                FullName = (string)reader["full_name"],
                Email = (string)reader["email"],
                Role = new Role()
                {
                    RoleCode = (string)reader["role_code"]
                },
            };
        }
        conn.Close();
        return user;
    }
}
