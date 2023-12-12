namespace Bts.Model;

internal class CandidateDocument : BaseModel
{
    public User Candidate { get; init; }
    public BTSFile File { get; init; }
    public DocumentType DocumentType { get; init; }
}
