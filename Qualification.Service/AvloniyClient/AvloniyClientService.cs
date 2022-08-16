using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Qualification.Service.AvloniyClient;

public class AvloniyClientService : IAvloniyClientService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://erp-integration.maktab.uz/api/v1/avloniy/";
    
    public AvloniyClientService(IConfiguration config)
    {
        _httpClient = new HttpClient();

        var username = config.GetSection("ERP:username").Value;
        var password = config.GetSection("ERP:password").Value;
        
        var svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
    }


    public async Task<bool> IsUserRegistered(string username, string password)
    {
        var response = await _httpClient.GetAsync(_baseUrl + $"?username={username}&password={password}");
        
        return response.IsSuccessStatusCode;
    }
    
    
}