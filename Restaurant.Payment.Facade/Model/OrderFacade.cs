using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Facade;

namespace Restaurant.Payment.Facade;

public class OrderFacade : IOrderFacade
{

    public Task SaveOrderToPayment(OrderDto data)
    {
        throw new NotImplementedException();
    }

}
