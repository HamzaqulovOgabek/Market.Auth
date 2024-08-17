namespace Market.Auth.Domain.Models;

public class UserDevice : BaseEntity<int>
{
    public int UserId { get; set; }
    public string Agent { get; set; }
    public string MacAddress { get; set; }
    public User User { get; set; }
}
