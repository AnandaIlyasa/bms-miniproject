using Bts.Model;

namespace Bts.IRepo;

internal interface IPackageRepo
{
    Package CreateNewPackage(Package package);
    List<Package> GetPackageList();
    List<Package> GetPackageListByReviewer(User reviewer);
}
