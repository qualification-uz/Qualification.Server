using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Qualification.Service.DTOs;

namespace Qualification.Service.AvloniyClient;

public class AvloniyClientService : IAvloniyClientService
{
    private readonly IConfiguration configuration;
    private readonly IHttpClientFactory httpClientFactory;
    
    public AvloniyClientService(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        this.configuration = configuration;
        this.httpClientFactory = httpClientFactory;
    }

    public async ValueTask<ERPResponse> IsUserRegistered(string username, string password)
    {
        using (var httpClient = this.httpClientFactory.CreateClient("avloniy"))
        {
            var content = await httpClient
                .GetStringAsync(GetUserRegistrationUrl(username, password));

            var eRPResponse = JsonConvert
                .DeserializeObject<ERPResponse>(content);

            return eRPResponse;
        }
    }

    private string GetUserRegistrationUrl(string username, string password) =>
        $"IsUserRegistered?username={username}&password={password}";
}