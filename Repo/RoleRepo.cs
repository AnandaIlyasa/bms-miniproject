namespace Bts.Repo;

using Bts.Constant;
using Bts.Helper;
using Bts.IRepo;
using Bts.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class RoleRepo : IRoleRepo
{
    readonly DatabaseHelper _dbHelper;

    public RoleRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public List<Role> GetRoleListExcludingSuperadminAndCandidate()
    {
        const string sqlQuery =
            "SELECT " +
                "id, " +
                "role_code, " +
                "role_name " +
            "FROM " +
                "t_m_role " +
            "WHERE " +
                "role_code != @sa_code " +
                "AND role_code != @can_code";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@sa_code", RoleCode.SuperAdmin);
        sqlCommand.Parameters.AddWithValue("@can_code", RoleCode.Candidate);
        var reader = sqlCommand.ExecuteReader();
        List<Role> roleList = new List<Role>();
        while (reader.Read())
        {
            var role = new Role()
            {
                Id = (int)reader["id"],
                RoleCode = (string)reader["role_code"],
                RoleName = (string)reader["role_name"],
            };
            roleList.Add(role);
        }

        conn.Close();

        return roleList;
    }

    public Role GetCandidateRole()
    {
        const string sqlQuery =
            "SELECT " +
                "id, " +
                "role_code, " +
                "role_name " +
            "FROM " +
                "t_m_role " +
            "WHERE " +
                "role_code = @can_code";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@can_code", RoleCode.Candidate);
        var reader = sqlCommand.ExecuteReader();
        var role = new Role();
        if (reader.Read())
        {
            role.Id = (int)reader["id"];
            role.RoleCode = (string)reader["role_code"];
            role.RoleName = (string)reader["role_name"];
        }

        conn.Close();

        return role;
    }
}
