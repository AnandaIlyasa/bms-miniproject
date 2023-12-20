using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface IRoleRepo
{
    List<Role> GetRoleListExcludingSuperadminAndCandidate(string superadminRoleCode, string candidateRoleCode, DBContextConfig context);
    Role GetCandidateRole(string candidateRoleCode, DBContextConfig context);
    Role GetReviewerRole(string reviewerRoleCode, DBContextConfig context);
}
