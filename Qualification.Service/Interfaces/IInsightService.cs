using Qualification.Service.DTOs.Insight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.Interfaces
{
    public interface IInsightService
    {
        ValueTask<QuizTopScoreInsightDto> GetCountOfTopInQuizResultAsync(int quizId);
        ValueTask<ApplicationStatusInsightDto> GetCountOfApplicationStatusAsync();
        ValueTask<ApplicationStatusInsightDto> GetCountOfApplicationStatusBySchoolIdAsync(int schoolId);
        ValueTask<ApplicationStatusInsightDto> GetCountOfApplicationStatusByTeacherIdAsync(int teacherId);
    }
}
