﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Data.Migrations;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Helpers;
using Qualification.Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository applicationRepository;
    private readonly IMapper mapper;
    private readonly IAssetService assetService;
    private readonly UserManager<User> userManager;

    public ApplicationService(
        IApplicationRepository applicationRepository,
        IMapper mapper,
        IAssetService assetService,
        UserManager<User> userManager)
    {
        this.applicationRepository = applicationRepository;
        this.mapper = mapper;
        this.assetService = assetService;
        this.userManager = userManager;
    }

    public async ValueTask<ApplicationDto> AddApplicationAsync(ApplicationForCreationDto applicationDto)
    {
        var application = this.mapper.Map<Application>(applicationDto);

        var teacher = await this.userManager.Users
            .Include(teacher => teacher.Applications)
            .FirstOrDefaultAsync(teacher =>
                teacher.Id == applicationDto.TeacherId);

        if(teacher is null)
            throw new NotFoundException("Couldn't find teacher for given id");

        bool isExpectedTeacher = await this.userManager
            .IsInRoleAsync(teacher, Enum.GetName<UserRole>(UserRole.Teacher));

        if (!isExpectedTeacher)
            throw new InvalidOperationException("Not allowed user");
        
        teacher.Applications.Add(application);

        application =
            await this.applicationRepository.InsertApplicationAsync(application);

        await this.userManager.UpdateAsync(teacher);

        return this.mapper.Map<ApplicationDto>(application);
    }

    public IEnumerable<ApplicationDto> RetrieveAllApplications(
        PaginationParams @params,
        Filters filters)
    {
        var applications = this.applicationRepository
            .SelectAllApplications();

        applications = filters
                .Aggregate(applications, (current, filter) => current.Filter(filter));

        applications = applications
            .Include(application => application.Groups)
            .Include(application => application.Teacher)
            .Include(paymentRequest => paymentRequest.PaymentRequests)
            .ThenInclude(application => application.Assets);
        
        return this.mapper.Map<IEnumerable<ApplicationDto>>(applications)
            .ToPagedList(@params);
    }

    public async ValueTask<ApplicationDto> RetrieveApplicationByIdAsync(long applicationId)
    {
        var application = await this.applicationRepository
            .SelectAllApplications()
            .Include(application => application.Groups)
            .Include(application => application.Teacher)
            .Include(application => application.PaymentRequests)
            .ThenInclude(payment => payment.Assets)
            .FirstOrDefaultAsync(application => application.Id == applicationId);

        if (application is null)
            throw new NotFoundException("Couldn't find application for given id");

        return this.mapper.Map<ApplicationDto>(application);
    }
    
    public async ValueTask<ApplicationDto> ModifyApplicationAsync(
        long applicationId,
        ApplicationForUpdateDto applicationDto)
    {
        var application =
            await this.applicationRepository.SelectApplicationByIdAsync(applicationId, new[] {"Teacher"});

        if (application is null)
            throw new NotFoundException("Couldn't find application for given id");

        if(applicationDto.TeacherId.HasValue)
        {
            var teacher = await this.userManager.Users
                .Include(teacher => teacher.Applications)
                .FirstOrDefaultAsync(teacher =>
                    teacher.Id == applicationDto.TeacherId);

            if (teacher is null)
                throw new NotFoundException("Couldn't find teacher for given id");

            bool isExpectedTeacher = await this.userManager
                .IsInRoleAsync(teacher, Enum.GetName<UserRole>(UserRole.Teacher));

            if (!isExpectedTeacher)
                throw new InvalidOperationException("Not allowed user");

            var existedTeacher = await this.userManager.Users
                .Include(teacher => teacher.Applications)
                .FirstOrDefaultAsync(teacher =>
                    teacher.Id == application.TeacherId);

            existedTeacher?.Applications.Remove(application);
            teacher.Applications.Add(application);

            await this.userManager.UpdateAsync(existedTeacher);
            await this.userManager.UpdateAsync(teacher);
        }

        if(applicationDto.SubjectId.HasValue)
            application.SubjectId = applicationDto.SubjectId.Value;
        if (applicationDto.DocumentId.HasValue)
            application.DocumentId = applicationDto.DocumentId.Value;

        if(applicationDto.Groups is not null)
        {
            application.Groups = applicationDto.Groups
            .Select(group => new Group
            {
                GradeId =  group.GradeId,
                GradeLetterId = group.GradeLetterId,
                GradeLetter = group.GradeLetter,
                SchoolYear = group.SchoolYear,
                SchoolYearId = group.SchoolYearId,
                Grade = group.Grade
            })
            .ToList();
        }

        application = await this.applicationRepository
            .UpdateApplicationAsync(application);

        return this.mapper.Map<ApplicationDto>(application);
    }

    public async ValueTask<ApplicationDto> RemoveApplicationAsync(long applicationId)
    {
        var application =
            await this.applicationRepository.SelectApplicationByIdAsync(applicationId);

        if (application is null)
            throw new NotFoundException("Couldn't find application for given id");

        application = await this.applicationRepository.DeleteApplicationAsync(application);

        return this.mapper.Map<ApplicationDto>(application);
    }

    public async ValueTask<ApplicationDto> ModifyApplicationStatusAsync(
        long applicationId,
        ApplicationStatus status)
    {
        var application = 
            await this.applicationRepository.SelectApplicationByIdAsync(applicationId);

        if (application is null)
            throw new NotFoundException("Couldn't find application for given id");

        application.Status = status;

        var updatedApplication = await this.applicationRepository
            .UpdateApplicationAsync(application);

        return this.mapper.Map<ApplicationDto>(updatedApplication);
    }

    public IEnumerable<RoleDto> RetrieveAllApplicationStatus()
    {

        var applicationStatusIds = Enum.GetValues<ApplicationStatus>();

        foreach (var applicationStatusId in applicationStatusIds)
            yield return new RoleDto
            {
                Id = (int)applicationStatusId,
                Name = Enum.GetName<ApplicationStatus>(applicationStatusId),
                Description = EnumHelper.GetDisplayValue((ApplicationStatus)applicationStatusId)
            };
    }

    public IEnumerable<ApplicationDto> RetrieveApplicationsForSchool(
        long schoolId,
        PaginationParams @params,
        Filter filter)
    {
        var applications = this.applicationRepository
            .SelectAllApplications()
            .Where(application => application.SchoolId == schoolId)
            .OrderBy(filter)
            .Include(application => application.Groups)
            .Include(application => application.Teacher)
            .OrderByDescending(application => application.CreatedAt)
            .ToPagedList(@params);

        return this.mapper.Map<IEnumerable<ApplicationDto>>(applications);
    }

    public IEnumerable<ApplicationDto> RetrieveApplicationsForTeacher(
        long teacherId,
        PaginationParams @params,
        Filters filters)
    {
        var applications = this.applicationRepository
            .SelectAllApplications()
            .Include(application => application.Groups)
            .Include(application => application.Teacher)
            .Include(request => request.PaymentRequests)
            .ThenInclude(payment => payment.Assets)
            .Where(application => application.TeacherId == teacherId)
            .OrderByDescending(application => application.CreatedAt)
            .AsQueryable();
           
        applications = filters.Aggregate(applications, (current, filter) => current.Filter(filter));
        
        return this.mapper.Map<IEnumerable<ApplicationDto>>(applications.ToPagedList(@params));
    }
}