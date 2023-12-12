namespace Bts.Model;

internal class MultipleChoiceOption : BaseModel
{
    public string OptionChar { get; init; }
    public string? OptionText { get; init; }
    public bool IsCorrect { get; set; }
    public Question Question { get; init; }
    public BTSFile? OptionImage { get; init; }
}
