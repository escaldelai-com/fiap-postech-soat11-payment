using Newtonsoft.Json;

namespace Restaurant.Payment.ExternalServices.Model.PagSeguro;

// Problema de serialização no envio para o PagSeguro
#pragma warning disable CA1707
#pragma warning disable IDE1006

public sealed class RequestQrCode
{

    public RequestQrCodeAmount amount { get; set; } = new();

    public DateTimeOffset expiration_date { get; set; }

}

#pragma warning restore IDE1006
#pragma warning restore CA1707
