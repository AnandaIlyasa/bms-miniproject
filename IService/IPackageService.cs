using Bts.Model;

namespace Bts.IService;

internal interface IPackageService
{
    List<Package> GetPackageList();
    List<Package> GetPackageListByReviewer(User reviewer);
    Package CreatePackage(Package package);
}
