using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.Facade;

public interface IOrderFacade
{

    Task SaveOrderToPayment(OrderDto data);

    Task Confirm(OrderDto data);

}
