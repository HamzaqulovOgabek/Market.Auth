namespace Market.Auth.Domain.Models;

public class Auditable<TId> : BaseEntity<TId> where TId : struct
{
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public int CreatedUserId { get; set; }
    public int ModifiedUserId { get; set; }
}
