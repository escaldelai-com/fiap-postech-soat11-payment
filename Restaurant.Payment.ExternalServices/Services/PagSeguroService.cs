using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.Presenter;
using Restaurant.Payment.ExternalServices.Model.PagSeguro;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Payment.ExternalServices.Services;

public class PagSeguroService(
    IJsonPresenter presenter,
    IConfiguration configuration) : IPagSeguroService
{

    private readonly CultureInfo enUS = new("en-US");

    private readonly string baseUrl = configuration["PagSeguro:BaseUrl"]
        ?? throw new ArgumentNullException("PagSeguro:BaseUrl");

    private readonly string token = configuration["PagSeguro:Token"]
        ?? throw new ArgumentNullException("PagSeguro:Token");



    public async Task<JObject?> SendPayment(OrderDto order, DefaultAddressDto address)
    {
        using var http = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/orders")
        {
            Content = GetContent(order, address)
        };
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return presenter.Deserialize<JObject>(json);
    }


    private JsonContent GetContent(OrderDto order, DefaultAddressDto address)
    {
        return JsonContent.Create(new RequestOrder
        {
            reference_id = order.Id!,
            customer = new RequestCustomer
            {
                name = order.Cliente?.Nome!,
                email = order.Cliente?.Email!,
                tax_id = order.Cliente?.CPF!
            },
            shipping = new RequestShipping
            {
                address = new RequestAddress
                {
                    street = address.Street!,
                    number = address.Number!,
                    postal_code = address.ZipCode!,
                    locality = address.Locality!,
                    city = address.City!,
                    region_code = address.RegionCode!,
                    country = address.Country!
                }
            },
            items = [.. order.Items.Select(item => new RequestItem
            {
                name = item.Nome!,
                quantity = 1,
                unit_amount = Convert.ToInt32(item.Preco!.Value * 100)
            })],
            qr_codes = [new RequestQrCode
            {
                expiration_date = DateTime.Now.AddHours(1),
                amount = new RequestQrCodeAmount
                {
                    value = Convert
                        .ToInt32(order.Items
                        .Sum(x => (x.Preco ?? 0) * 100))
                        .ToString(enUS)
                }
            }],
            notification_urls = [
                configuration["PagSeguro:NotificationUrl"]!
            ]
        });
    }

}
