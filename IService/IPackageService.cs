using Bts.Model;

namespace Bts.IService;

internal interface IPackageService
{
    List<Package> GetPackageList();
    Package CreatePackage(Package package);
}
