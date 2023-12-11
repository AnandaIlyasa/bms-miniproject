namespace bms.Model;

internal class CandidateDocument : BaseModel
{
    public User Candidate { get; init; }
    public File File { get; init; }
    public DocumentType DocumentType { get; init; }
}
