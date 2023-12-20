using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

[Table("t_m_question")]
internal class Question : BaseModel
{
    [Column("question")]
    public string? QuestionContent { get; set; }

    [Column("package_id")]
    public int PackageId { get; set; }

    [ForeignKey(nameof(PackageId))]
    public Package Package { get; set; }

    [Column("image_id")]
    public int? ImageId { get; set; }

    [ForeignKey(nameof(ImageId))]
    public BTSFile? Image { get; set; }

    [NotMapped]
    public List<MultipleChoiceOption> OptionList { get; set; } // not mapped
}
