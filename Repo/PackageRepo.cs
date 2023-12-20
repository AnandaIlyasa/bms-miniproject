using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using Microsoft.EntityFrameworkCore;

namespace Bts.Repo;

internal class PackageRepo : IPackageRepo
{
    public Package CreateNewPackage(Package package, DBContextConfig context)
    {
        context.Packages.Add(package);
        context.SaveChanges();
        return package;
    }

    public List<Package> GetPackageList(DBContextConfig context)
    {
        var packageList = context.Packages.ToList();
        return packageList;
    }

    public List<Package> GetPackageListByReviewer(User reviewer, DBContextConfig context)
    {
        var packageList = context.Packages
                            .FromSql($@"SELECT 
                                            p.* 
                                        FROM 
                                            t_m_package p
                                        JOIN 
                                            t_r_exam_package ep ON p.id = ep.package_id
                                        JOIN 
	                                        t_r_exam e ON ep.exam_id = e.id
                                        JOIN 
	                                        t_m_user u ON e.reviewer_id = u.id
                                        WHERE 
	                                        e.reviewer_id = {reviewer.Id}")
                            .ToList();

        return packageList;
    }
}
