using Newtonsoft.Json;

namespace Restaurant.Payment.ExternalServices.Model.PagSeguro;


// Problema de serialização no envio para o PagSeguro
#pragma warning disable CA1707
#pragma warning disable IDE1006

public sealed class RequestAddress
{

    public string street { get; set; } = string.Empty;

    public string number { get; set; } = string.Empty;

    public string locality { get; set; } = string.Empty;

    public string city { get; set; } = string.Empty;

    public string region_code { get; set; } = string.Empty;

    public string country { get; set; } = string.Empty;

    public string postal_code { get; set; } = string.Empty;
}

#pragma warning restore IDE1006
#pragma warning restore CA1707
