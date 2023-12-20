namespace Bts.IRepo;

using Bts.Config;
using Bts.Model;

internal interface IFileRepo
{
    BTSFile CreateNewFile(BTSFile file, DBContextConfig context);
}
