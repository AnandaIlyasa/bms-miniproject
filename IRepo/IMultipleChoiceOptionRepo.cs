using Bts.Model;

namespace Bts.IRepo;

internal interface IMultipleChoiceOptionRepo
{
    MultipleChoiceOption CreateNewOption(MultipleChoiceOption option);
}
