using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Users;

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

    public async ValueTask<ERPResponse<SchoolDto>> IsUserRegistered(string username, string password)
    {
        using (var httpClient = this.httpClientFactory.CreateClient("avloniy"))
        {
            var content = await httpClient
                .GetStringAsync(GetUserRegistrationUrl(username, password));

            var eRPResponse = JsonConvert
                .DeserializeObject<ERPResponse<SchoolDto>>(content);

            return eRPResponse;
        }
    }

    public async ValueTask<ERPResponse<IEnumerable<SubjectDto>>> SelectAllSubjectsAsync()
    {
        using (var httpClient = this.httpClientFactory.CreateClient("avloniy"))
        {
            var content = await httpClient
                .GetStringAsync(GetSchoolSubjectUrl());

            var eRPResponse = JsonConvert
                .DeserializeObject<ERPResponse<IEnumerable<SubjectDto>>>(content);

            return eRPResponse;
        }
    }

    public async ValueTask<ERPResponse<IEnumerable<GradeDto>>> SelectAllGradesAsync()
    {
        using (var httpClient = this.httpClientFactory.CreateClient("avloniy"))
        {
            var content = await httpClient
                .GetStringAsync(GetSchoolGradeUrl());

            var eRPResponse = JsonConvert
                .DeserializeObject<ERPResponse<IEnumerable<GradeDto>>>(content);

            return eRPResponse;
        }
    }

    public async ValueTask<ERPResponse<IEnumerable<GradeLetterDto>>> SelectAllGradeLettersAsync()
    {
        using (var httpClient = this.httpClientFactory.CreateClient("avloniy"))
        {
            var content = await httpClient
                .GetStringAsync(GetSchoolGradeLetterUrl());

            var eRPResponse = JsonConvert
                .DeserializeObject<ERPResponse<IEnumerable<GradeLetterDto>>>(content);

            return eRPResponse;
        }
    }

    public async ValueTask<ERPResponse<IEnumerable<SchoolYearDto>>> SelectAllSchoolYearsAsync()
    {
        using (var httpClient = this.httpClientFactory.CreateClient("avloniy"))
        {
            var content = await httpClient
                .GetStringAsync(GetSchoolYeaUrl());

            var eRPResponse = JsonConvert
                .DeserializeObject<ERPResponse<IEnumerable<SchoolYearDto>>>(content);

            return eRPResponse;
        }
    }
    
    private string GetSchoolSubjectUrl() => $"GetSchoolSubject";
    private string GetSchoolGradeUrl() => $"GetAllschoolgrade";
    private string GetSchoolGradeLetterUrl() => $"GetAllSchoolgradeletter";
    private string GetSchoolYeaUrl() => $"GetAllSchoolyear";

    private string GetUserRegistrationUrl(string username, string password) =>
        $"IsUserRegistered?username={username}&password={password}";
}