namespace Bts.Model;

abstract class BaseModel
{
    public int Id { get; set; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public int Ver { get; init; }
    public bool IsActive { get; init; }
}
