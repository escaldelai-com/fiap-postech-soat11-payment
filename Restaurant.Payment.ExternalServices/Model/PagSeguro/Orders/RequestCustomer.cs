using Newtonsoft.Json;

namespace Restaurant.Payment.ExternalServices.Model.PagSeguro;

// Problema de serialização no envio para o PagSeguro
#pragma warning disable CA1707
#pragma warning disable IDE1006

public sealed class RequestCustomer
{

    public string name { get; set; } = string.Empty;

    public string email { get; set; } = string.Empty;

    public string tax_id { get; set; } = string.Empty;

}

#pragma warning restore IDE1006
#pragma warning restore CA1707
