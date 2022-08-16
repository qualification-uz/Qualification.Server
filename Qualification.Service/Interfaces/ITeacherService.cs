using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Interfaces;

public interface ITeacherService
{
    Task<Teacher> CreateAsync(UserForCreationDto dto);

    /// <summary>
    /// Generate Student Accounts
    /// </summary>
    /// <param name="count"></param>
    /// <param name="classId">Should have at least one element</param>
    /// <returns>Returns Excel File: username, login, password columns</returns>
    Task<IFormFile> GenerateAccountsAsync(int count, int[] classId);
    Task<bool> DeleteAsync(Expression<Func<Teacher, bool>> expression);
    Task<IEnumerable<Teacher>> GetAllAsync(PaginationParams @params, Expression<Func<Teacher, bool>>? expression = null);
    Task<Teacher> GetAsync(Expression<Func<Teacher, bool>> expression);
}