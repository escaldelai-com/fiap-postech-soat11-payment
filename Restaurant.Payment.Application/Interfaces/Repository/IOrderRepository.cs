using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.Repository;

public interface IOrderRepository
{

    Task<OrderDto> Create(OrderDto order);

    Task<OrderDto> Update(OrderDto order);

}
