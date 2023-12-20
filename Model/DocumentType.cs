using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

[Table("t_m_document_type")]
[Index(nameof(Code), IsUnique = true, Name = "document_type_bk")]
internal class DocumentType : BaseModel
{
    [Column("code"), MaxLength(5)]
    public string Code { get; set; }

    [Column("type_name"), MaxLength(30)]
    public string TypeName { get; set; }
}
