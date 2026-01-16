using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.Presenter;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Application.Interfaces.UseCases;
using Restaurant.Payment.Domain;
using Restaurant.Payment.Domain.Model;

namespace Restaurant.Payment.Application.UseCases;

public class OrderPagSeguroUseCase(
    IOrderRepository repo,
    IDefaultAddressRepository addressRepo,
    IPaymentInfoRepository infoRepo,
    IPixInfoPresenter pixInfoPresenter,
    IIdentificationService idService,
    IPagSeguroService service) : IOrderPagSeguroUseCase
{

    public async Task<PixInfoDto> SendPayment(string orderId)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(orderId)
            .Validate();

        var data = await repo.GetById(orderId);

        if (data is null)
            throw new NotFoundException(orderId);
        else if (data.Status != OrderStatus.WaitingPayment)
            throw new OrderStatusException(data.Status!, "send payment to PagSeguro");
        else if (data.Cliente?.Id == null)
            throw new ValidationException(["Client information is missing."]);

        var clientId = data.Cliente.Id;

        data.Cliente = await idService.GetById(clientId);

        if (data.Cliente == null)
            throw new NotFoundException(clientId);

        var address = await addressRepo.GetById(1)
            ?? throw new ValidationException(["Default address not found."]);

        var payment = new PaymentInfoDto
        {
            PaymentData = await service.SendPayment(data, address),
            OrderId = orderId,
            UpdateDate = DateTime.Now,
            Status = PaymentStatus.Sent
        };

        if (payment.PaymentData == null)
            throw new ValidationException(["Invalid response from pagseguro."]);

        await infoRepo.Create(payment);

        return pixInfoPresenter.GetPixInfo(payment.PaymentData);
    }

}
