using Newtonsoft.Json.Linq;

namespace Restaurant.Payment.Application.Interfaces.UseCases;

public interface IOrderFinalizeUseCase
{

    Task Finalize(JObject data);

}
