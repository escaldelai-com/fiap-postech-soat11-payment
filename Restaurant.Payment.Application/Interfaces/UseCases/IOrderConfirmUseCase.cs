using Newtonsoft.Json.Linq;

namespace Restaurant.Payment.Application.Interfaces.UseCases;

public interface IOrderConfirmUseCase
{

    Task Confirm(JObject data);

}