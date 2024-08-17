namespace Market.Auth.DataAccess.Repositories.UserRepo;

public class OrderBaseDto
{
    public OrderItemDtoBase OrderItemDtoBase { get; set; } = null!;
    public string ShippingAddress { get; set; } = null!;
    public int UserId { get; set; }
    public int CartId { get; set; }
    public int TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }

}
