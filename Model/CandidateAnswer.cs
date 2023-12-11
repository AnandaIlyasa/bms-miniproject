namespace bms.Model;

internal class CandidateAnswer : BaseModel
{
    public string? AnswerContent { get; set; }
    public Package ExamPackage { get; init; }
    public MultipleChoiceOption? ChoiceOption { get; init; }
    public Question Question { get; init; }

    public CandidateAnswer(string? answerContent, Package examPackage, MultipleChoiceOption? choiceOption, Question question)
    {
        AnswerContent = answerContent;
        ExamPackage = examPackage;
        ChoiceOption = choiceOption;
        Question = question;
    }
}
