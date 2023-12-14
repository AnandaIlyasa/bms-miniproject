namespace Bts.Model;

internal class Exam : BaseModel
{
    public User Candidate { get; set; }
    public User Reviewer { get; set; }
    public DateTime LoginStart { get; set; }
    public DateTime LoginEnd { get; set; }
    public AcceptanceStatus? AcceptanceStatus { get; set; }
    public ExamPackage ExamPackage { get; set; } // not mapped
    public List<CandidateAnswer> CandidateAnswerList { get; set; } // not mapped
    public List<Question> QuestionList { get; set; }
}
