using Bts.Config;
using Bts.IRepo;
using Bts.Model;

namespace Bts.Repo;

internal class FileRepo : IFileRepo
{
    public BTSFile CreateNewFile(BTSFile file, DBContextConfig context)
    {
        context.BTSFiles.Add(file);
        context.SaveChanges();
        return file;
    }
}
