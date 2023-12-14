namespace Bts.IRepo;

using Bts.Model;

internal interface IFileRepo
{
    BTSFile CreateNewFile(BTSFile file);
}
