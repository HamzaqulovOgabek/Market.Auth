namespace Market.Auth.DataAccess.Repositories.UserRepo;

public class OrderItemDtoBase
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }

}
