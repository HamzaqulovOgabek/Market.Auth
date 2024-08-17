namespace Market.Auth.Domain.Models;

public class UserToken : BaseEntity<int>
{
    public int UserId { get; set; }
    public string? Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? RevokedDate { get; set; }
    public User? User { get; set; }
}
