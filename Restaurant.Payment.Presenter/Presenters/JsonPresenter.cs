using Newtonsoft.Json;
using Restaurant.Payment.Application.Interfaces.Presenter;

namespace Restaurant.Payment.Presenter;

public class JsonPresenter : IJsonPresenter
{

    public string? Serialize(object? data)
    {
        return data == null
            ? default : JsonConvert.SerializeObject(data);
    }

    public T? Deserialize<T>(string? json)
    {
        return string.IsNullOrEmpty(json)
            ? default : JsonConvert.DeserializeObject<T>(json);
    }

}
