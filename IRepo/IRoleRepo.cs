using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface IRoleRepo
{
    List<Role> GetRoleList(DBContextConfig context);
    Role GetRoleByCode(string roleCode, DBContextConfig context);
}
