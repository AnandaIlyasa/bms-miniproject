namespace bms.Model;

internal class ExamPackage : BaseModel
{
    public Package Package { get; set; }
    public Exam Exam { get; init; }
    public DateTime? ExamStartDateTime { get; set; }
    public int Duration { get; set; }
    public bool? IsSubmitted { get; set; }
    public string? ReviewerNotes { get; set; }
    public float? ReviewerScore { get; set; }

    public ExamPackage(Package package, Exam exam, DateTime examStartDateTime, int duration)
    {
        Package = package;
        Exam = exam;
        ExamStartDateTime = examStartDateTime;
        Duration = duration;
    }
}
