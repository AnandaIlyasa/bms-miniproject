using System.Data.SqlClient;
using System.Data;
using Bts.Model;

namespace Bts.Helper;

internal class DatabaseHelper
{

    const string host = "LAPTOP-4GBC7O34\\SQLEXPRESS";
    const string db = "bts";
    const string connString = $"Data Source={host}; Initial Catalog={db}; Integrated Security=true;";
    SqlConnection? conn;

    public SqlConnection GetConnection()
    {
        if (conn == null)
        {
            conn = new SqlConnection(connString);
        }
        return conn;
    }

    //public int ExecuteNonQuery(string commandText, SqlParameter[] commandParameters)
    //{
    //    using (var conn = new SqlConnection(connString))
    //    {
    //        var sqlCommand = conn.CreateCommand();
    //        sqlCommand.CommandText = commandText;
    //        sqlCommand.Parameters.AddRange(commandParameters);
    //        conn.Open();
    //        var affectedRows = sqlCommand.ExecuteNonQuery();
    //        return affectedRows;
    //    }
    //}

    //public void ExecuteQuery<T>(string commandText, SqlParameter[] commandParameters)
    //{
    //    var conn = new SqlConnection(connString);
    //    conn.Open();
    //    var sqlCommand = conn.CreateCommand();
    //    sqlCommand.CommandText = commandText;
    //    sqlCommand.Parameters.AddRange(commandParameters);
    //    var reader = sqlCommand.ExecuteReader();
    //    T? user;
    //    while (reader.Read())
    //    {
    //        var obj = new
    //        {
    //            FullName = (string)reader["full_name"],
    //            Email = (string)reader["email"],
    //            Role = new Role()
    //            {
    //                RoleCode = (string)reader["role_code"]
    //            },
    //        };
    //    }
    //    //user = (T) obj;
    //    conn.Close();
    //}
}
