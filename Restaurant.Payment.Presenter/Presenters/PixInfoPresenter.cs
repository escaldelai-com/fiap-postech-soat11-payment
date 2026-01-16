using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Presenter;
using Restaurant.Payment.Domain;

namespace Restaurant.Payment.Presenter;

public class PixInfoPresenter : IPixInfoPresenter
{

    public PixInfoDto GetPixInfo(JObject info)
    {
        var qrCodes = info["qr_codes"] as JArray;

        if (qrCodes is null || qrCodes.Count == 0)
            throw new ValidationException(["Missing pix qr code information."]);

        if (qrCodes[0] is not JObject qrCode)
            throw new ValidationException(["Invalid pix qr code information."]);

        var links = qrCode["links"] as JArray;

        return new PixInfoDto
        {
            OrderId = info.Value<string>("id"),
            ExpirationDate = ParseExpirationDate(qrCode),
            Code = qrCode.Value<string>("text"),
            Image = GetLinkHref(links, "QRCODE.PNG"),
            Base64 = GetLinkHref(links, "QRCODE.BASE64")
        };
    }


    private static DateTime? ParseExpirationDate(JObject qrCode)
    {
        var expirationText = qrCode.Value<string>("expiration_date");

        if (string.IsNullOrWhiteSpace(expirationText))
            return null;

        return DateTimeOffset.TryParse(expirationText, out var expiration)
            ? expiration.UtcDateTime
            : null;
    }

    private static string? GetLinkHref(JArray? links, string rel)
    {
        if (links is null)
            return null;

        foreach (JToken link in links)
        {
            if (link is not JObject linkObject)
                continue;

            if (string.Equals(linkObject.Value<string>("rel"), rel, StringComparison.OrdinalIgnoreCase))
                return linkObject.Value<string>("href");
        }

        return null;
    }

}
