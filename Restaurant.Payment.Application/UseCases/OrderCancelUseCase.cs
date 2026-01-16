using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Application.Interfaces.UseCases;
using Restaurant.Payment.Domain;

namespace Restaurant.Payment.Application.UseCases;

public class OrderCancelUseCase(
    IOrderRepository repo,
    IOrderService orderService,
    IPaymentInfoRepository payRepo) : IOrderCancelUseCase
{

    public async Task Cancel(JObject data)
    {
        Validate(data);

        var orderId = data!.Value<string>("reference_id")!;
        var order = await repo.GetById(orderId)
            ?? throw new NotFoundException(orderId);

        var model = new Order(
            order.Data!.Value,
            order.Cliente!.Id!,
            order.Status!,
            [.. order.Items.Select(x => new OrderItem(
                x.Nome!,
                x.Tipo!,
                x.Preco!.Value))
            ]);

        model.Cancel();
        order.Status = model.Status;

        await repo.Update(order);
        await payRepo.Create(new PaymentInfoDto
        {
            OrderId = orderId,
            Status = order.Status,
            UpdateDate = DateTime.Now,
            PaymentData = data
        });
        await orderService.Cancel(order);
    }


    private static void Validate(JObject data)
    {
        var validator = Validator.Create();

        validator.IsNotNull(data).Validate();
        validator.Test(data.ContainsKey("reference_id"), "Invalid Payment - No reference_id").Validate();
    }

}