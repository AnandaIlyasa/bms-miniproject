using Bts.Config;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class AcceptanceStatusRepo : IAcceptanceStatusRepo
{
    public List<AcceptanceStatus> GetAcceptanceStatusList(DBContextConfig context)
    {
        return context.AcceptanceStatuses.ToList();
    }

    public AcceptanceStatus GetAcceptanceStatusByCode(string code, DBContextConfig context)
    {
        return context.AcceptanceStatuses.First(x => x.Code == code);
    }
}
