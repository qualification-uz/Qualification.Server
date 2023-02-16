using Qualification.Domain.Entities.Users;

namespace Qualification.Service.Interfaces;

public interface IExcelService
{
    Task<byte[]> ExportStudentsAsync(List<Student> students);
}