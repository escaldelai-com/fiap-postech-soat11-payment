using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.UseCases;

public interface IOrderPagSeguroUseCase
{

    Task<PixInfoDto> SendPayment(string orderId);

}
