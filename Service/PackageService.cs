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
        var newPackage = _packageRepo.CreateNewPackage(package);
        return newPackage;
    }

    public List<Package> GetPackageList()
    {
        var packageList = _packageRepo.GetPackageList();
        return packageList;
    }

    public List<Package> GetPackageListByReviewer(User reviewer)
    {
        var packageList = _packageRepo.GetPackageListByReviewer(reviewer);
        return packageList;
    }
}
