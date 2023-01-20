using OfficeOpenXml;
using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly ISchoolService schoolService;

        public ReportService(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

        private async Task<MemoryStream> GenerateReportAsync<T>(IEnumerable<T> data, List<string> columnNames = null, string reportName = "Report")
        {
            // Get column name of data
            var systemColumnNames = typeof(T).GetProperties().Select(p => p.Name).ToList();
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            
            // Create a new Excel package
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add(reportName);

            // Add the column names to the worksheet
            for (int i = 0; i < columnNames.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = columnNames[i];
            }

            // Add the data to the worksheet
            for (int i = 0; i < data.Count(); i++)
            {
                for (int j = 0; j < columnNames.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1].Value = data.ElementAt(i).GetType().GetProperty(systemColumnNames[j]).GetValue(data.ElementAt(i), null);
                }
            }

            // Save the package to a MemoryStream
            var stream = new MemoryStream();
            await package.SaveAsAsync(stream);
            stream.Position = 0;

            return stream;
        }

        /// <summary>
        /// Generate teachers to excel report
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="params"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<MemoryStream> ExportTeachersToExcelAsync(int schoolId, PaginationParams @params, Filters filters)
        {
            var teachers = schoolService.RetrieveAllTeachers(schoolId, @params, filters);
            var columnNames = new List<string> { "Id", "Ism", "Familiya", "Sharif", "Telefon raqami", "Login", "RoleId", "Role" };
            
            return await GenerateReportAsync<UserDto>(teachers, columnNames, "Teachers");
        }

        /// <summary>
        /// Export schools to excel report
        /// </summary>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<MemoryStream> ExportSchoolsToExcelAsync(PaginationParams @params, Filters filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Export applications to excel report
        /// </summary>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<MemoryStream> ExportApplicationsToExcelAsync(PaginationParams @params, Filters filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate applications by schoolId to excel report
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<MemoryStream> ExportApplicationsBySchoolIdToExcelAsync(int schoolId, PaginationParams @params, Filters filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate applications by teacherId to excel
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<MemoryStream> ExportApplicationsByTeacherIdToExcelAsync(int teacherId, PaginationParams @params, Filters filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate payments to excel reports
        /// </summary>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<MemoryStream> ExportPaymentsToExcelAsync(PaginationParams @params, Filters filter)
        {
            throw new NotImplementedException();
        }
    }
}
