namespace Bts.Repo;

using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using System.Collections.Generic;

internal class RoleRepo : IRoleRepo
{
    public List<Role> GetRoleListExcludingSuperadminAndCandidate(string superadminRoleCode, string candidateRoleCode, DBContextConfig context)
    {
        var roleList = context.Roles
                        .Where(r => r.RoleCode != candidateRoleCode && r.RoleCode != superadminRoleCode)
                        .ToList();
        return roleList;
    }

    public Role GetCandidateRole(string candidateRoleCode, DBContextConfig context)
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
