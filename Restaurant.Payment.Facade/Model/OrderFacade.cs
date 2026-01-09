using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.Facade;

namespace Restaurant.Payment.Facade;

public class OrderFacade(
    IOrderService service) : IOrderFacade
{
    public Task Confirm(OrderDto data)
    {
        throw new NotImplementedException();
    }

    public async Task SaveOrderToPayment(OrderDto data)
    {
        await service.ConfirmPayment(data);
    }

}
