namespace Market.Auth.Domain.Models;

public class RolePermission : BaseEntity<int>
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public Role Role { get; set; }
    public Permission Permission { get; set; }
}
