using Newtonsoft.Json;

namespace Restaurant.Payment.ExternalServices.Model.PagSeguro;

// Problema de serialização no envio para o PagSeguro
#pragma warning disable IDE1006

public sealed class RequestQrCodeAmount
{

    public string value { get; set; } = string.Empty;

}

#pragma warning restore IDE1006
