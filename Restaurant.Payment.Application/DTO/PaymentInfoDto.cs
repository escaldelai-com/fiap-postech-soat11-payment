using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Application.DTO;

public class PaymentInfoDto
{

    public int? Id { get; set; }

    public string? OrderId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Status { get; set; }

    public JObject? PaymentData { get; set; }


}
