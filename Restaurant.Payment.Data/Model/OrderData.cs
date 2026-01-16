using Newtonsoft.Json.Linq;

namespace Restaurant.Payment.Data.Model;

public class OrderData
{

    public string? Id { get; set; }

    public DateTime? Data { get; set; }

    public DateTime? DataPagamento { get; private set; }

    public string? Status { get; set; }

    public string? Cliente { get; set; }

    public List<OrderItemData> Items { get; set; } = [];

    public List<PaymentInfoData> PaymentInfo { get; set; } = [];

}
