
namespace Market.Auth.Domain.Models
{
    public class Role : Auditable<int>
    {
        public required string Name { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}
