namespace Bts.Model;

internal class Exam : BaseModel
{
    public User Candidate { get; init; }
    public User Reviewer { get; init; }
    public DateTime LoginStart { get; init; }
    public DateTime LoginEnd { get; init; }
    public AcceptanceStatus? AcceptanceStatus { get; set; }
}
