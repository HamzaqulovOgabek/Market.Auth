using Market.Auth.Domain.Enums;

namespace Market.Auth.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class PermissionGroupName : Attribute
{
    public PermissionGroup Group { get; set; }

    public PermissionGroupName(PermissionGroup group)
    {
        Group = group;
    }
}
