using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.Repository;

public interface IOrderRepository
{

    Task<OrderDto> GetById(string id);

    Task<OrderDto> Create(OrderDto order);

    Task<OrderDto> Update(OrderDto order);

}
