using Bts.Config;
using Bts.Helper;
using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class PackageService : IPackageService
{
    readonly IPackageRepo _packageRepo;
    readonly SessionHelper _sessionHelper;

    public PackageService(IPackageRepo packageRepo, SessionHelper sessionHelper)
    {
        _packageRepo = packageRepo;
        _sessionHelper = sessionHelper;
    }

    public Package CreatePackage(Package package)
    {
        using (var context = new DBContextConfig())
        {
            package.CreatedBy = _sessionHelper.UserId;
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
}
