namespace Qualification.Service.DTOs.Application;

public class GroupDto
{
    public long Id { get; set; }
    
    public string Grade { get; set; }
    public int GradeId { get; set; }

    public string GradeLetter { get; set; }
    public int GradeLetterId { get; set; }

    public string SchoolYear { get; set; }
    public int SchoolYearId { get; set; }
}
