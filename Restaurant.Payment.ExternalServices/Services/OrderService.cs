using Microsoft.Extensions.Configuration;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.WebApi;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Payment.ExternalServices.Services;

public class OrderService(
    ISecurityService security,
    IConfiguration configuration) : IOrderService
{

    private readonly string baseUrl = configuration["ExternalServices:Order"]
        ?? throw new ArgumentNullException("ExternalServices:Order");


    public async Task ConfirmPayment(string orderId)
    {
        using var http = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/order/pay");

        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", security.Token);
        message.Content = JsonContent.Create(new { orderId });

        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();
    }

}
