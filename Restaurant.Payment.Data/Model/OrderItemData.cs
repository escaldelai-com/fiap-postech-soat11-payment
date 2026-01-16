namespace Restaurant.Payment.Data.Model;

public class OrderItemData
{

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public decimal? Price { get; set; }

    public OrderData? Order { get; set; }

}
