using Market.Auth.Domain.Attributes;

namespace Market.Auth.Domain.Enums;

public enum Permission
{
    [PermissionGroupName(PermissionGroup.User)]
    UserCreate = 1,
    [PermissionGroupName(PermissionGroup.User)]
    UserUpdate = 2,
    [PermissionGroupName(PermissionGroup.User)]
    UserDelete = 3,
    [PermissionGroupName(PermissionGroup.User)]
    UserView = 4,

    [PermissionGroupName(PermissionGroup.Role)]
    RoleCreate = 5,
    [PermissionGroupName(PermissionGroup.Role)]
    RoleUpdate = 6,
    [PermissionGroupName(PermissionGroup.Role)]
    RoleDelete = 7,
    [PermissionGroupName(PermissionGroup.Role)]
    RoleView = 8,

    [PermissionGroupName(PermissionGroup.UserDevice)]
    UserDeviceDelete = 9,
    [PermissionGroupName(PermissionGroup.UserDevice)]
    UserDeviceView = 10,

    [PermissionGroupName(PermissionGroup.Permission)]
    PermissionView = 11,

}
