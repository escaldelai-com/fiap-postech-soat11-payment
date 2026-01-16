using System.Text.Json;

namespace Restaurant.Payment.Application.Interfaces.Presenter;

public interface IJsonPresenter
{

    string? Serialize(object? data);

    T? Deserialize<T>(string? json);

}
