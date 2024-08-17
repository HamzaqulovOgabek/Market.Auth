namespace Market.Auth.Domain.Models;

public class UserRole : BaseEntity<int>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public DateTime CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; }
    public Role Role{ get; set; }

}
