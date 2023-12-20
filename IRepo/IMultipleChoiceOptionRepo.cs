using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface IMultipleChoiceOptionRepo
{
    MultipleChoiceOption CreateNewOption(MultipleChoiceOption option, DBContextConfig context);
    List<MultipleChoiceOption> GetOptionListByQuestion(int questionId, DBContextConfig context);
}
