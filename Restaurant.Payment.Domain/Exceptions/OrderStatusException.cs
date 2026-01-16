namespace Restaurant.Payment.Domain;

public class OrderStatusException(string status, string operation) : Exception
{

    public override string Message => $"Operation '{operation}' is not allowed on an order with status '{status}'";

}
