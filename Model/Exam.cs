using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

[Table("t_r_exam")]
internal class Exam : BaseModel
{
    [Column("candidate_id")]
    public int CandidateId { get; set; }

    [ForeignKey(nameof(CandidateId))]
    public User Candidate { get; set; }

    [Column("reviewer_id")]
    public int ReviewerId { get; set; }

    [ForeignKey(nameof(ReviewerId))]
    public User Reviewer { get; set; }

    [Column("login_start")]
    public DateTime LoginStart { get; set; }

    [Column("login_end")]
    public DateTime LoginEnd { get; set; }

    [Column("acceptance_status_id")]
    public int? AcceptanceStatusId { get; set; }

    [ForeignKey(nameof(AcceptanceStatusId))]
    public AcceptanceStatus? AcceptanceStatus { get; set; }

    [NotMapped]
    public ExamPackage ExamPackage { get; set; } // not mapped

    [NotMapped]
    public List<CandidateAnswer> CandidateAnswerList { get; set; } // not mapped

    [NotMapped]
    public List<Question> QuestionList { get; set; } // not mapped
}
