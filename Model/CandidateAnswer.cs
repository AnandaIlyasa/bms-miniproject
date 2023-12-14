namespace Bts.Model;

internal class CandidateAnswer : BaseModel
{
    public ExamPackage ExamPackage { get; init; }
    public Question Question { get; init; }
    public string? AnswerContent { get; set; }
    public MultipleChoiceOption? ChoiceOption { get; set; }
}
