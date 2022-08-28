using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Qualification.Service.DTOs.Application;
public class ApplicationForCreationDto
{
    public short AttandancePercent { get; set; }
    public int SubjectId { get; set; }
    public long SchoolId { get; set; }
    public IFormFile Document { get; set; }
    public long TeacherId { get; set; }
    
    [FromForm]
    public ICollection<GroupForCreationDto> Groups { get; set; }
}
