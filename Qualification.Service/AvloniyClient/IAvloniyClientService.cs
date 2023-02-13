using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.AvloniyClient;

public interface IAvloniyClientService
{
    ValueTask<ERPResponse<SchoolDto>> IsUserRegistered(string username, string password);
    ValueTask<ERPResponse<IEnumerable<SubjectDto>>> SelectAllSubjectsAsync();
    ValueTask<ERPResponse<IEnumerable<GradeDto>>> SelectAllGradesAsync();
    ValueTask<ERPResponse<IEnumerable<GradeLetterDto>>> SelectAllGradeLettersAsync();
    ValueTask<ERPResponse<IEnumerable<SchoolYearDto>>> SelectAllSchoolYearsAsync();
    ValueTask<ERPResponse<TeacherFromErpDto>> SelectTeacherByPinflAsync(string pinfl);
    ValueTask<List<StudentDto>> SelectStudentsAsync(long schoolId, List<GroupForCreationDto> groups);
}