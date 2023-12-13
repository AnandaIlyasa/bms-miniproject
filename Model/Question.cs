namespace Bts.Model;

internal class Question : BaseModel
{
    public string? QuestionContent { get; init; }
    public Package Package { get; init; }
    public BTSFile? Image { get; init; }
    public List<MultipleChoiceOption> OptionList { get; set; } // not mapped
}
