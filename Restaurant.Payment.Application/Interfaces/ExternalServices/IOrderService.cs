using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.ExternalServices;

public interface IOrderService
{

    Task ConfirmPayment(OrderDto order);

    Task Cancel(OrderDto order);

}
