using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface IPackageRepo
{
    Package CreateNewPackage(Package package, DBContextConfig context);
    List<Package> GetPackageList(DBContextConfig context);
    List<Package> GetPackageListByReviewer(User reviewer, DBContextConfig context);
}
