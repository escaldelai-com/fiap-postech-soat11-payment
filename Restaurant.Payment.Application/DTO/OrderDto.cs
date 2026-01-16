using Newtonsoft.Json.Linq;

namespace Restaurant.Payment.Application.DTO;

public class OrderDto
{

    public string? Id { get; set; }

    public DateTime? Data { get; set; }

    public string? Status { get; set; }

    public ClientDto? Cliente { get; set; }

    public List<OrderItemDto> Items { get; set; } = [];

    public List<PaymentInfoDto> PaymentInfo { get; set; } = [];

}
