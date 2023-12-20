using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

abstract class BaseModel
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; } = 1;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Timestamp, Column("ver")]
    public byte[] Ver { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;
}
