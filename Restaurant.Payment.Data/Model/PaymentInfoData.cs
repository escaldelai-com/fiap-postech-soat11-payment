using Restaurant.Payment.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Data;

public class PaymentInfoData
{

    public int? Id { get; set; }

    public OrderData? Order { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Status { get; set; }

    public string? PaymentData { get; set; }

}
