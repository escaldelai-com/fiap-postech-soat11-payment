using Newtonsoft.Json;

namespace Restaurant.Payment.ExternalServices.Model.PagSeguro;

// Problema de serialização no envio para o PagSeguro
#pragma warning disable CA1707
#pragma warning disable IDE1006

public sealed class RequestItem
{

    public string name { get; set; } = string.Empty;

    public int quantity { get; set; }

    public decimal unit_amount { get; set; }

}

#pragma warning restore IDE1006
#pragma warning restore CA1707
