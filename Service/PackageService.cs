using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class PackageService : IPackageService
{
    public Package CreatePackage(Package package)
    {
        return new Package();
    }

    public List<Package> GetPackageList()
    {
        return new List<Package>();
    }
}
