using Bogus;
using Bogus.Extensions.Brazil;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Domain;
namespace Restaurant.Payment.Application.Test;

public abstract class TestBase
{
    protected Faker Faker { get; } = new("pt_BR");

    protected static string GetGuid()
    {
        return Guid.NewGuid().ToString("n");
    }

    protected OrderDto GetOrder()
    {
        return new OrderDto
        {
            Id = GetGuid(),
            Status = OrderStatus.WaitingPayment,
            Cliente = new ClientDto
            {
                Id = GetGuid(),
                Nome = Faker.Name.FullName(),
                CPF = Faker.Person.Cpf(),
                Email = Faker.Internet.Email()
            },
            Data = Faker.Date.Past(),
            Items = [.. Faker.Make(3, () => new OrderItemDto
            {
                Id = GetGuid(),
                Nome = Faker.Commerce.ProductName(),
                Tipo = Faker.Commerce.Categories(1)[0],
                Preco = Faker.Random.Decimal(10, 100)
            })]
        };
    }

    protected static JObject GetPaidPayment()
    {
        return JObject.Parse("""
        {
            "reference_id":"6967092abd5e8adcd09d9196",
            "charges": [{
                "status": "PAID",
                "payment_response": {
                    "code": "20000"
                }
            }]
        }
        """);
    }

    protected static JObject GetCanceledPayment()
    {
        return JObject.Parse("""
        {
            "reference_id":"6967092abd5e8adcd09d9196",
            "charges": [{
                "status": "CANCELED",
                "payment_response": {
                    "code": "20000"
                }
            }]
        }
        """);
    }

    protected static JObject? GetPayment(string? json)
    {
        return json == null
            ? null
            : JObject.Parse(json);
    }

}
