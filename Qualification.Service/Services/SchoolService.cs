﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Data.Repositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.AvloniyClient;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class SchoolService : ISchoolService
{
    private readonly IAvloniyClientService avloniyClientService;
    private readonly UserManager<User> userManager;
    private readonly IApplicationRepository applicationRepository;
    private readonly IQuizRepository quizRepository;
    private readonly IMapper mapper;

    public SchoolService(
        IAvloniyClientService avloniyClientService,
        IMapper mapper,
        UserManager<User> userManager,
        IApplicationRepository applicationRepository,
        IQuizRepository quizRepository)
    {
        this.avloniyClientService = avloniyClientService;
        this.mapper = mapper;
        this.userManager = userManager;
        this.applicationRepository = applicationRepository;
        this.quizRepository = quizRepository;
    }

    public IEnumerable<UserDto> RetrieveAllTeachers(
        int schoolId,
        PaginationParams paginationParams,
        Filters filters)
    {
        var users = this.userManager.Users;

        users = filters
                .Aggregate(users, (current, filter) => current.Filter(filter));

        var pagedList = users.Where(user => user.SchoolId == schoolId)
            .OrderByDescending(user => user.Id)
            .ToPagedList(paginationParams);

        return this.mapper.Map<IEnumerable<UserDto>>(pagedList);
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

    public async ValueTask<UserDto> RetrieveTeacherByPinflAsync(
        int schoolId,
        TeacherPinflDto teacherPinflDto)
    {
        var eRPResponse = await this.avloniyClientService
            .SelectTeacherByPinflAsync(teacherPinflDto.PINFL);

        if (!eRPResponse.Success || eRPResponse.Result is null)
            throw new NotFoundException("Coudn't find teacher with this PINFL");

        var mappedTeacher = this.mapper.Map<TeacherForCreationDto>(eRPResponse.Result);
        mappedTeacher.PINFL = teacherPinflDto.PINFL;
        mappedTeacher.Password = teacherPinflDto.Password;

        return await AddTeacherAsync(schoolId, mappedTeacher);
    }

    public async ValueTask<UserDto> AddTeacherAsync(int schoolId, TeacherForCreationDto teacherDto)
    {
        var userExists = await this.userManager.FindByNameAsync(teacherDto.PINFL);

        if (userExists is not null)
            throw new AlreadyExistsException("User already exists with the same PINFL");

        User user = GenerateNewUser(teacherDto);
        user.SchoolId = schoolId;
        var result = await this.userManager.CreateAsync(user, teacherDto.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException(message:
                string.Join("", result.Errors.Select(error => error.Description)));

        await this.userManager.AddToRoleAsync(user, Enum.GetName(UserRole.Teacher));

        var mappedUser = this.mapper.Map<UserDto>(user);
        mappedUser.RoleId = UserRole.Teacher;
        mappedUser.Role = Enum.GetName(UserRole.Teacher);

        return mappedUser;
    }

    public async ValueTask<bool> RemoveTeacherAsync(int schoolId, long teacherId)
    {
        var user = await this.userManager.FindByIdAsync(teacherId.ToString());

        // Deletes all Applications related to this teacher
        var applications = applicationRepository.SelectAllApplications()
            .Where(a => a.TeacherId == user.Id);

        foreach(var application in applications)
            await applicationRepository.DeleteApplicationAsync(application);
        
        // Deletes all quizes related to this teacher
        var quizes = quizRepository.SelectAllQuizzes()
            .Where(a => a.UserId == user.Id);
        foreach(var quiz in quizes)
            await quizRepository.DeleteQuizAsync(quiz);

        if (user is null)
            throw new NotFoundException("Coudn't find teacher with this ID");

        if (user.SchoolId != schoolId)
            throw new Exception("You don't have permission to delete this teacher");

        var result = await this.userManager.DeleteAsync(user);

        if (!result.Succeeded)
            throw new InvalidOperationException(message:
                string.Join("", result.Errors.Select(error => error.Description)));

        return true;
    }
}
