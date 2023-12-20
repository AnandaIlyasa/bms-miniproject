using Bts.Config;
using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class PackageService : IPackageService
{
    readonly IPackageRepo _packageRepo;

    public PackageService(IPackageRepo packageRepo)
    {
        _packageRepo = packageRepo;
    }

    public Package CreatePackage(Package package)
    {
        using (var context = new DBContextConfig())
        {
            package = _packageRepo.CreateNewPackage(package, context);
        }
        return package;
    }

    public List<Package> GetPackageList()
    {
        var packageList = new List<Package>();
        using (var context = new DBContextConfig())
        {
            packageList = _packageRepo.GetPackageList(context);
        }
        return packageList;
    }

    public List<Package> GetPackageListByReviewer(User reviewer)
    {
        var packageList = new List<Package>();
        using (var context = new DBContextConfig())
        {
            packageList = _packageRepo.GetPackageListByReviewer(reviewer, context);
        }
        packageList = packageList
            .GroupBy(p => p.Id)
            .Select(p => p.First())
            .ToList();
        return packageList;
    }
}
