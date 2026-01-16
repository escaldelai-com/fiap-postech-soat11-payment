using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;

namespace Restaurant.Payment.Application.Interfaces.Presenter;

public interface IPixInfoPresenter
{

    PixInfoDto GetPixInfo(JObject info);

}
