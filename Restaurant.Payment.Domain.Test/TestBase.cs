using Bogus;

namespace Restaurant.Payment.Application.Test;

public abstract class TestBase
{
    protected Faker Faker { get; } = new("pt_BR");

    protected static string GetGuid()
    {
        return Guid.NewGuid().ToString("n");
    }

}
