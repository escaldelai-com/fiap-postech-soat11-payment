namespace Restaurant.Payment.Application.Interfaces.ExternalServices;

public interface IOrderService
{

    Task ConfirmPayment(string orderId);

}
