using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.UseCases;

public interface IOrderCreateUseCase
{

	Task<OrderDto> Create(OrderDto order);

}
