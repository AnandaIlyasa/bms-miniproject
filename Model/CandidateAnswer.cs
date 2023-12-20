using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

[Table("t_r_candidate_answer")]
[Index(nameof(ExamPackageId), nameof(ChoiceOptionId), nameof(QuestionId), IsUnique = true, Name = "candidate_document_ck")]
internal class CandidateAnswer : BaseModel
{
    [Column("answer_content")]
    public string? AnswerContent { get; set; }

    [Column("exam_package_id")]
    public int ExamPackageId { get; set; }

    [ForeignKey(nameof(ExamPackageId))]
    public ExamPackage ExamPackage { get; set; }

    [Column("choice_option_id")]
    public int? ChoiceOptionId { get; set; }

    [ForeignKey(nameof(ChoiceOptionId))]
    public MultipleChoiceOption? ChoiceOption { get; set; }

    [Column("question_id")]
    public int QuestionId { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public Question Question { get; set; }
}
