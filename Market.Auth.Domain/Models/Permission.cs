namespace Market.Auth.Domain.Models;

public class Permission : BaseEntity<int>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required int PermissionGroupId { get; set; }

    public PermissionGroup PermissionGroup { get; set; }
}
