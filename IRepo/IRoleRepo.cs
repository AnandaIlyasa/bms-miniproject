using Bts.Model;

namespace Bts.IRepo;

internal interface IRoleRepo
{
    List<Role> GetAllRoleExcludingSuperadminAndCandidate();
}
