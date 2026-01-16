using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.Facade;

public interface IOrderFacade
{

    Task<OrderDto> SaveOrderToPayment(OrderDto data);

    Task<PixInfoDto> SendPayment(string orderId);

    Task Confirm(JObject data);

}
