namespace Bts.Model;

internal class User : BaseModel
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Pass { get; set; }
    public BTSFile? Photo { get; init; }
    public Role Role { get; init; }
}
