namespace Market.Auth.Application.Services.PermissionServices;

public class PermissionBaseDto
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public int PermissionGroupId { get; set; }
}
