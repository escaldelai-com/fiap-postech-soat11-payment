using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using System.Text.Json;

namespace Restaurant.Payment.Application.Interfaces.ExternalServices;

public interface IPagSeguroService
{

    Task<JObject?> SendPayment(OrderDto order, DefaultAddressDto address);

}
