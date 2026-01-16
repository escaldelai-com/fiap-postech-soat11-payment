namespace Restaurant.Payment.Domain;

public class OrderStatus
{

    public const string Elaboration = "Em elaboração";

    public const string WaitingPayment = "Aguardando pagamento";

    public const string Paid = "Pago";

    public const string Canceled = "Cancelado";

    public const string Received = "Recebido";

    public const string Preparing = "Em preparação";

    public const string Delivery = "Entrega";

    public const string Done = "Concluído";

}
