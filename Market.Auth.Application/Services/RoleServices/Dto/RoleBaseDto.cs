namespace Market.Auth.Application.Services.RoleService;

public class RoleBaseDto
{
    public required string Name { get; set; }
    public List<int> PermissionIds { get; set; }
}
