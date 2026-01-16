using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Application.Interfaces.UseCases;
using Restaurant.Payment.Domain;

namespace Restaurant.Payment.Application.UseCases;

public class OrderCreateUseCase(
    IOrderRepository repo) : IOrderCreateUseCase
{

    public async Task<OrderDto> Create(OrderDto order)
    {
        Validator.Create()
            .IsNotNull(order)
            .IsNotNull(order?.Data!)
            .Validate();

        _ = new Order(
            order!.Data!.Value,
            order.Cliente?.Id!,
            order.Status!,
            [.. order.Items
                .Select(item => new OrderItem(
                    item.Nome!,
                    item.Tipo!,
                    item.Preco!.Value))]);

        return await repo.Create(order);
    }

}
