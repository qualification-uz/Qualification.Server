using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

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
        Filter filter)
    {
        var applications = this.applicationRepository
            .SelectAllApplications()
            .OrderBy(filter)
            .Include(application => application.Groups)
            .Include(application => application.Teacher)
            .ToPagedList(@params);

        return this.mapper.Map<IEnumerable<ApplicationDto>>(applications);
    }

    public async ValueTask<ApplicationDto> RetrieveApplicationByIdAsync(long applicationId)
    {
        var application = await this.applicationRepository
            .SelectApplicationByIdAsync(applicationId, new[] { "Groups", "Teacher" } );

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
}