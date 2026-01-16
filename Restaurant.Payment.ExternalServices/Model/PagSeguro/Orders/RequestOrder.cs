using Newtonsoft.Json;

namespace Restaurant.Payment.ExternalServices.Model.PagSeguro;

// Problema de serialização no envio para o PagSeguro
#pragma warning disable CA1707
#pragma warning disable IDE1006

public sealed class RequestOrder
{

    public RequestCustomer customer { get; set; } = new();

    public IList<RequestItem> items { get; set; } = [];

    public RequestShipping shipping { get; set; } = new();

    public IList<RequestQrCode> qr_codes { get; set; } = [];

    public string reference_id { get; set; } = string.Empty;

    public List<string> notification_urls { get; set; } = [];

}

#pragma warning restore IDE1006
#pragma warning restore CA1707
