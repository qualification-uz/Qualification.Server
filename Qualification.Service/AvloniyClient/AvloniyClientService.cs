using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Qualification.Service.DTOs;

namespace Qualification.Service.AvloniyClient;

public class AvloniyClientService : IAvloniyClientService
{
    private const string BASE_URL = "https://erp-integration.maktab.uz/api/v1/avloniy";
    private readonly IConfiguration configuration;
    
    public AvloniyClientService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async ValueTask<ERPResponse> IsUserRegistered(string username, string password)
    {
        using (var httpClient = GetHttpClient())
        {
            var content = await httpClient
                .GetStringAsync(GetUserRegistrationUrl(username, password));

            var eRPResponse = JsonConvert
                .DeserializeObject<ERPResponse>(content);

            return eRPResponse;
        }
    }

    private string GetUserRegistrationUrl(string username, string password) =>
        $"{BASE_URL}/IsUserRegistered?username={username}&password={password}";

    private HttpClient GetHttpClient()
    {

        HttpClientHandler clientHandler = new HttpClientHandler();

        clientHandler.ServerCertificateCustomValidationCallback =
            (sender, cert, chain, sslPolicyErrors) => { return true; };

        var httpClient = new HttpClient(clientHandler);
        var username = this.configuration.GetSection("ERP:username").Value;
        var password = configuration.GetSection("ERP:password").Value;
        var svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);

        return httpClient;
    }
}