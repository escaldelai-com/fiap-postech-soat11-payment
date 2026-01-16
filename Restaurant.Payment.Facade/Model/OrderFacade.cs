using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.Facade;
using Restaurant.Payment.Application.Interfaces.UseCases;

namespace Restaurant.Payment.Facade;

public class OrderFacade(
    IOrderCreateUseCase useCase,
    IIdentificationService idService,
    IOrderPagSeguroUseCase pagSeguroUseCase,
    IOrderConfirmUseCase confirmUseCase) : IOrderFacade
{

    public async Task<OrderDto> SaveOrderToPayment(OrderDto data)
    {
        var result = await useCase.Create(data);

        result.Cliente = await idService.GetById(data.Cliente?.Id);

        return result;
    }

    public async Task<PixInfoDto> SendPayment(string orderId)
    {
        return await pagSeguroUseCase.SendPayment(orderId);
    }

    public async Task Confirm(JObject data)
    {
        await confirmUseCase.Confirm(data);
    }

}
