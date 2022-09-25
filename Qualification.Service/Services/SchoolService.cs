﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.AvloniyClient;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class SchoolService : ISchoolService
{
    private readonly IAvloniyClientService avloniyClientService;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public SchoolService(
        IAvloniyClientService avloniyClientService,
        IMapper mapper,
        UserManager<User> userManager)
    {
        this.avloniyClientService = avloniyClientService;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    public async ValueTask<UserDto> AddTeacherAsync(int schoolId, TeacherForCreationDto teacherDto)
    {
        var userExists = await userManager.FindByNameAsync(teacherDto.PINFL);

        if (userExists is not null)
            throw new AlreadyExistsException("User already exists with the same PINFL");

        User user = GenerateNewUser(teacherDto);
        user.SchoolId = schoolId;
        var result = await userManager.CreateAsync(user, teacherDto.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException(message:
                string.Join("", result.Errors.Select(error => error.Description)));

        await userManager.AddToRoleAsync(user, Enum.GetName(UserRole.Teacher));

        var mappedUser = this.mapper.Map<UserDto>(user);
        mappedUser.RoleId = UserRole.Teacher;
        mappedUser.Role = Enum.GetName(UserRole.Teacher);

        return mappedUser;
    }

    public IEnumerable<UserDto> RetrieveAllTeachers(int schoolId)
    {
        var users = this.userManager.Users.Where(user => user.SchoolId == schoolId);

        return this.mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async ValueTask<IEnumerable<GradeLetterDto>> RetrieveAllGradeLettersAsync()
    {
        var eRPReponse = await this.avloniyClientService.SelectAllGradeLettersAsync();

        if (eRPReponse?.ResultCount <= 0)
            throw new NotFoundException("Coudn't find any grade letters");

        return eRPReponse.Result;
    }

    public async ValueTask<IEnumerable<GradeDto>> RetrieveAllGradesAsync()
    {
        var eRPReponse = await this.avloniyClientService.SelectAllGradesAsync();

        if (eRPReponse?.ResultCount <= 0)
            throw new NotFoundException("Coudn't find any grades");

        return eRPReponse.Result;
    }

    public async ValueTask<IEnumerable<SchoolYearDto>> RetrieveAllSchoolYearsAsync()
    {
        var eRPReponse = await this.avloniyClientService.SelectAllSchoolYearsAsync();

        if (eRPReponse?.ResultCount <= 0)
            throw new NotFoundException("Coudn't find any school years");

        return eRPReponse.Result;
    }

    public async ValueTask<IEnumerable<SubjectDto>> RetrieveAllSubjectsAsync()
    {
        var eRPReponse = await this.avloniyClientService.SelectAllSubjectsAsync();

        if (eRPReponse?.ResultCount <= 0)
            throw new NotFoundException("Coudn't find any subjects");

        return eRPReponse.Result;
    }

    private static User GenerateNewUser(TeacherForCreationDto teacherDto)
    {
        return new User()
        {
            FirstName = teacherDto.FirstName,
            LastName = teacherDto.LastName,
            MiddleName = teacherDto.MiddleName,
            PhoneNumber = teacherDto.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = teacherDto.PINFL
        };
    }

    public async ValueTask<UserDto> RetrieveTeacherByPinfl(
        int schoolId, 
        TeacherPinflDto teacherPinflDto)
    {
        var existingTeacher = await avloniyClientService.SelectTeacherByPinflAsync(teacherPinflDto.PINFL);

        if (!existingTeacher.Success)
            throw new NotFoundException("Coudn't find teacher with this PINFL");

        var mappedTeacher = mapper.Map<TeacherForCreationDto>(existingTeacher);

        return await AddTeacherAsync(schoolId, mappedTeacher);
    }
}
