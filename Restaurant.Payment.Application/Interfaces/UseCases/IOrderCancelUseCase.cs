using Newtonsoft.Json.Linq;

namespace Restaurant.Payment.Application.Interfaces.UseCases;

public interface IOrderCancelUseCase
{

    Task Cancel(JObject data);

}