﻿using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Interfaces;

public interface ISchoolService
{
    ValueTask<IEnumerable<SubjectDto>> RetrieveAllSubjectsAsync();
    ValueTask<IEnumerable<GradeDto>> RetrieveAllGradesAsync();
    ValueTask<IEnumerable<GradeLetterDto>> RetrieveAllGradeLettersAsync();
    ValueTask<IEnumerable<SchoolYearDto>> RetrieveAllSchoolYearsAsync();
    IEnumerable<UserDto> RetrieveAllTeachers(int schoolId);
    ValueTask<UserDto> AddTeacherAsync(int schoolId, TeacherForCreationDto teacherDto);
    ValueTask<UserDto> RetrieveTeacherByPinflAsync(int schoolId, TeacherPinflDto teacherPinflDto);
}
