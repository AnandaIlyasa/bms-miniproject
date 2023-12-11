namespace bms.Model;

internal class Question : BaseModel
{
    public string? QuestionContent { get; init; }
    public Package Package { get; init; }
    public File? Image { get; init; }
}
