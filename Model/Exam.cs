namespace bms.Model;

internal class Exam : BaseModel
{
    public User Candidate { get; init; }
    public User Reviewer { get; set; }
    public DateTime LoginStart { get; set; }
    public DateTime LoginEnd { get; set; }
    public AcceptanceStatus? AcceptanceStatus { get; set; }

    public Exam(User candidate, User reviewer, DateTime loginStart, DateTime loginEnd, AcceptanceStatus? acceptanceStatus)
    {
        Candidate = candidate;
        Reviewer = reviewer;
        LoginStart = loginStart;
        LoginEnd = loginEnd;
        AcceptanceStatus = acceptanceStatus;
    }
}
