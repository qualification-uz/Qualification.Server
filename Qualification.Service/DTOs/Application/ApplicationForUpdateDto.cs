using Microsoft.AspNetCore.Http;

namespace Qualification.Service.DTOs.Application;

public class ApplicationForUpdateDto
{
    public short? AttandancePercent { get; set; }
    public int? SubjectId { get; set; }
    public long? DocumentId { get; set; }
    public long? TeacherId { get; set; }
}
