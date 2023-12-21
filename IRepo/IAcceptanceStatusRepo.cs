namespace Bts.IRepo;

using Bts.Config;
using Bts.Model;

internal interface IAcceptanceStatusRepo
{
    List<AcceptanceStatus> GetAcceptanceStatusList(DBContextConfig context);
    AcceptanceStatus GetAcceptanceStatusByCode(string code, DBContextConfig context);
}
