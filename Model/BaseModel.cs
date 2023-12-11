namespace bms.Model;

abstract class BaseModel
{
    public int Id { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public int Ver { get; init; }
    public bool IsActive { get; init; }
}
