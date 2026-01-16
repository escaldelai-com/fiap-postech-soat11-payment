namespace Restaurant.Payment.Domain;

public class Order
{

    public DateTime Data { get; private set; }

    public DateTime? DataPagamento { get; private set; }

    public string Cliente { get; private set; }

    public string Status { get; private set; }

    public OrderItem[] Items { get; private set; }

    public Order(DateTime data, string cliente, string status, OrderItem[] items)
    {
        Validator.Create()
            .IsInThePastOrPresent(data)
            .IsNotNullOrWhiteSpace(cliente)
            .IsNotNullOrWhiteSpace(status)
            .IsNotNull(items)
            .GreaterThanZero(items?.Length ?? 0)
            .Validate();

        Data = data;
        Cliente = cliente;
        Status = status;
        Items = items!;
    }


    public void ConfirmPay()
    {
        if (Status != OrderStatus.WaitingPayment)
            throw new OrderStatusException(Status, "confirm payment");

        Status = OrderStatus.Paid;
        DataPagamento = DateTime.Now;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Canceled)
            throw new OrderStatusException(Status, "confirm payment");

        Status = OrderStatus.Canceled;
        DataPagamento = DateTime.Now;
    }

}
