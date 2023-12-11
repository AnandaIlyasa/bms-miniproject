namespace bms.Model;

internal class MultipleChoiceOption : BaseModel
{
    public string OptionChar { get; set; }
    public string? OptionText { get; set; }
    public bool IsCorrect { get; set; }
    public Question Question { get; set; }
    public File? OptionImage { get; set; }

    public MultipleChoiceOption(string optionChar, bool isCorrect, Question question)
    {
        OptionChar = optionChar;
        IsCorrect = isCorrect;
        Question = question;
    }
}
