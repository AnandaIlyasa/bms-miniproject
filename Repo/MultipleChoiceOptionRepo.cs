using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using Microsoft.EntityFrameworkCore;

namespace Bts.Repo;

internal class MultipleChoiceOptionRepo : IMultipleChoiceOptionRepo
{
    public MultipleChoiceOption CreateNewOption(MultipleChoiceOption option, DBContextConfig context)
    {
        context.MultipleChoiceOptions.Add(option);
        context.SaveChanges();
        return option;
    }

    public List<MultipleChoiceOption> GetOptionListByQuestion(int questionId, DBContextConfig context)
    {
        var optionList = context.MultipleChoiceOptions
                            .Where(mco => mco.QuestionId == questionId)
                            .Include(mco => mco.OptionImage)
                            .ToList();
        return optionList;
    }
}
