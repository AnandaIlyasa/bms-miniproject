using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

[Table("t_r_exam_package")]
internal class ExamPackage : BaseModel
{
    [Column("package_id")]
    public int PackageId { get; set; }

    [ForeignKey(nameof(PackageId))]
    public Package Package { get; set; }

    [Column("exam_id")]
    public int ExamId { get; set; }

    [ForeignKey(nameof(ExamId))]
    public Exam Exam { get; set; }

    [Column("duration")]
    public int Duration { get; set; }

    [Column("exam_start_datetime")]
    public DateTime? ExamStartDateTime { get; set; }

    [Column("is_submitted")]
    public bool? IsSubmitted { get; set; }

    [Column("reviewer_notes")]
    public string? ReviewerNotes { get; set; }

    [Column("reviewer_score")]
    public double? ReviewerScore { get; set; }
}
