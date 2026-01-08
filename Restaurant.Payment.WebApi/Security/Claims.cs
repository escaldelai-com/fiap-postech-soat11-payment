namespace Restaurant.Payment.WebApi.Security;

public class Claims
{

    public class Order
    {
        public const string Pay = "payment:order:pay";
        public const string PayConfirm = "payment:order:pay-confirm";
    }


    public static string[] All = [
        Order.Pay,
        Order.PayConfirm
    ];

}
