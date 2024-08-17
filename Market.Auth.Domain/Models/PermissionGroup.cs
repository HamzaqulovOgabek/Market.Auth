namespace Market.Auth.Domain.Models;

public class PermissionGroup : BaseEntity<int>
{
    public required string Name  { get; set; }
    public required string Code { get; set; }
}
