namespace Bts.Repo;

using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using System.Collections.Generic;

internal class RoleRepo : IRoleRepo
{
    public List<Role> GetRoleList(DBContextConfig context)
    {
        var roleList = context.Roles.ToList();
        return roleList;
    }

    public Role GetRoleByCode(string candidateRoleCode, DBContextConfig context)
    {
        var candidateRole = context.Roles.Where(r => r.RoleCode == candidateRoleCode).First();
        return candidateRole;
    }

    public Role GetReviewerRole(string reviewerRoleCode, DBContextConfig context)
    {
        var reviewerRole = context.Roles.Where(r => r.RoleCode == reviewerRoleCode).First();
        return reviewerRole;
    }
}
