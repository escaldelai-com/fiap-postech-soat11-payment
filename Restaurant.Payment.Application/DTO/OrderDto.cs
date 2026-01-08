namespace Restaurant.Payment.Application.DTO;

public class OrderDto
{

    public string? Id { get; set; }

    public DateTime? Data { get; set; }

    public int? Numero { get; set; }

    public string? Status { get; set; }

    public ClientDto? Cliente { get; set; }

    public List<OrderItemDto> Items { get; set; } = new();


}
