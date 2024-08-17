using Market.Auth.Domain.Enums;
using Market.Auth.Domain.Models.Base;

namespace Market.Auth.Domain.Models;

public class User : BaseEntity<int>, IHaveState //: Auditable<int>
{
    //Do I need to inherit from Auditable?
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public string? Salt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    public State State { get; set; } = State.Active;
}
