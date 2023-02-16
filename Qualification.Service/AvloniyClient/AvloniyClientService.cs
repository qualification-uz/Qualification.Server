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

    public async ValueTask<ERPResponse<TeacherFromErpDto>> SelectTeacherByPinflAsync(string pinfl)
    {
        using (var httpClient = this.httpClientFactory.CreateClient("avloniy"))
        {
            var content = await httpClient.
                GetStringAsync(GetUserInfoWithPinfl(pinfl));

            var eRPResponse = JsonConvert
                .DeserializeObject<ERPResponse<TeacherFromErpDto>>(content);

            return eRPResponse;
        }
    }

    public async ValueTask<List<StudentDto>> SelectStudentsAsync(
        long schoolId,
        List<GroupForCreationDto> groups)
    {
        using (var httpClient = this.httpClientFactory.CreateClient("avloniy"))
        {
            var tasks = new Task<ERPResponse<IEnumerable<StudentDto>>>[groups.Count];

            for (int i = 0; i < groups.Count; i++)
            {
                string url = string.Format(
                    GetStudentsUrl(),
                    schoolId,
                    groups[i].GradeId,
                    groups[i].GradeLetterId,
                    groups[i].SchoolYearId);

                tasks[i] = httpClient
                    .GetStringAsync(url)
                    .ContinueWith(response => JsonConvert
                        .DeserializeObject<ERPResponse<IEnumerable<StudentDto>>>(response.Result));
            }

            var responses = await Task.WhenAll(tasks);

            return responses
                .Where(r => r.ResultCount > 0)
                .SelectMany(r => r.Result)
                .ToList();
        }
    }

    private string GetSchoolSubjectUrl() => $"GetSchoolSubject";
    private string GetSchoolGradeUrl() => $"GetAllschoolgrade";
    private string GetSchoolGradeLetterUrl() => $"GetAllSchoolgradeletter";
    private string GetSchoolYeaUrl() => $"GetAllSchoolyear";
    private string GetUserInfoWithPinfl(string pinfl) =>
        $"GetPersonInfoWithPINFL?pinfl={pinfl}";

    private string GetUserRegistrationUrl(string username, string password) =>
        $"IsUserRegistered?username={username}&password={password}";

    private string GetStudentsUrl() =>
        "GetOrgChildren?schoolid={0}&schoolgradeid={1}&schoolgradeletterid={2}&schoolyearid={3}";
}