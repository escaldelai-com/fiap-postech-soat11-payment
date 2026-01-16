namespace Restaurant.Payment.Application.DTO;

public class PixInfoDto
{

    public string? OrderId { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public string? Code { get; set; }

    public string? Image { get; set; }

    public string? Base64 { get; set; }

}
