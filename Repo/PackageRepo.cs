﻿using Bts.Constant;
using Bts.Helper;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class PackageRepo : IPackageRepo
{
    readonly DatabaseHelper _dbHelper;

    public PackageRepo(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public Package CreateNewPackage(Package package)
    {
        const string sqlQuery = "INSERT INTO " +
            "t_m_package(package_code, package_name, created_by, created_at, ver, is_active) VALUES " +
            "(@package_code, @package_name, @created_by, @created_at, @ver, @is_active) " +
            "SELECT @@identity";

        var conn = _dbHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@package_code", package.PackageCode);
        sqlCommand.Parameters.AddWithValue("@package_name", package.PackageName);
        sqlCommand.Parameters.AddWithValue("@created_by", package.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@created_at", package.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@ver", package.Ver);
        sqlCommand.Parameters.AddWithValue("@is_active", package.IsActive);

        conn.Open();
        var newPackageId = (int)(decimal)sqlCommand.ExecuteScalar();
        conn.Close();

        package.Id = newPackageId;
        return package;
    }

    public List<Package> GetPackageList()
    {
        const string sqlQuery =
            "SELECT " +
                "id, " +
                "package_name, " +
                "package_code " +
            "FROM " +
                "t_m_package";

        var conn = _dbHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        var reader = sqlCommand.ExecuteReader();
        List<Package> packageList = new List<Package>();
        while (reader.Read())
        {
            var package = new Package()
            {
                Id = (int)reader["id"],
                PackageCode = (string)reader["package_code"],
                PackageName = (string)reader["package_name"],
            };
            packageList.Add(package);
        }

        conn.Close();

        return packageList;
    }
}