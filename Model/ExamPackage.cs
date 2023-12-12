namespace Bts.Model;

internal class ExamPackage : BaseModel
{
    public Package Package { get; init; }
    public Exam Exam { get; init; }
    public int Duration { get; init; }
    public DateTime? ExamStartDateTime { get; set; }
    public bool? IsSubmitted { get; set; }
    public string? ReviewerNotes { get; set; }
    public float? ReviewerScore { get; set; }
}
