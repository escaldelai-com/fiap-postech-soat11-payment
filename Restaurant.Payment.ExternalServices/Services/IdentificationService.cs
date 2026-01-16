using Microsoft.Extensions.Configuration;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.Presenter;
using Restaurant.Payment.Application.Interfaces.WebApi;
using System.Net.Http.Headers;
using System.Web;

namespace Restaurant.Payment.ExternalServices;

public class IdentificationService(
    ISecurityService security,
    IJsonPresenter presenter,
    IConfiguration configuration) : IIdentificationService
{

    private readonly string baseUrl = configuration["ExternalServices:Identification"]
        ?? throw new ArgumentNullException("ExternalServices:Identification");


    public async Task<ClientDto?> GetByCpf(string? cpf)
    {
        using var http = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/client/cpf/{cpf}");

        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", security.Token);

        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return presenter.Deserialize<ClientDto>(content);
    }

    public async Task<ClientDto?> GetById(string? id)
    {
        using var http = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/client/id/{id}");

        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", security.Token);

        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return presenter.Deserialize<ClientDto>(content);
    }

    public async Task<IEnumerable<ClientDto>> GetByIds(IEnumerable<string> ids)
    {
        using var http = new HttpClient();
        var query = GetQueryIds(ids);
        var message = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/client/list{query}");
        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return presenter.Deserialize<IEnumerable<ClientDto>>(content) ?? [];
    }



    private static string GetQueryIds(IEnumerable<string> ids)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        foreach (var id in ids)
            query.Add("id", id);

        var result = query.ToString();

        return !string.IsNullOrEmpty(result)
            ? $"?{result}"
            : string.Empty;
    }

}
