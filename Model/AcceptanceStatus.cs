using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

[Table("t_m_acceptance_status")]
[Index(nameof(Code), IsUnique = true, Name = "acceptance_status_bk")]
internal class AcceptanceStatus : BaseModel
{
    [Column("code"), MaxLength(5)]
    public string Code { get; set; }

    [Column("status_name"), MaxLength(20)]
    public string StatusName { get; set; }
}
