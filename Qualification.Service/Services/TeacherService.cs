using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Qualification.Data.IRepositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities.Users;
using Qualification.Service.AvloniyClient;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAvloniyClientService _avloniyClient;

    public TeacherService(IUnitOfWork unitOfWork, IMapper mapper, IAvloniyClientService avloniyClient)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _avloniyClient = avloniyClient;
    }

    public async Task<Teacher> CreateAsync(UserForCreationDto dto)
    {
        // Checking From ERP system for user existing
        if (await _avloniyClient.IsUserRegistered(dto.Login, dto.Password))
            throw new HttpStatusCodeException(400, "User is already exist");
            
        // checking for existing locally
        var existUser = await _unitOfWork.Teachers.FirstOrDefaultAsync(user =>
            user.Login.Equals(dto.Login));
        
        if (existUser != null)
            throw new HttpStatusCodeException(400, "User is already exist");

        // hash password
        var user = _mapper.Map<Teacher>(dto);
        user.Password = user.Password.Encrypt();
        
        var result = await _unitOfWork.Teachers.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public Task<IFormFile> GenerateAccountsAsync(int count, int[] classId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Expression<Func<Teacher, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Teacher>> GetAllAsync(PaginationParams @params, Expression<Func<Teacher, bool>>? expression = null)
    {
        throw new NotImplementedException();
    }

    public Task<Teacher> GetAsync(Expression<Func<Teacher, bool>> expression)
    {
        throw new NotImplementedException();
    }
}
