namespace bms.Model;

internal class User : BaseModel
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Pass { get; init; }
    public File? Photo { get; init; }
    public Role Role { get; init; }
}
