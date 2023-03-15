using Microsoft.AspNetCore.Mvc.RazorPages;
using Qualification.Data.IRepositories;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Insight;
using Qualification.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.Services
{
#pragma warning disable
    public class InsightService : IInsightService
    {
        private readonly IQuizResultRepository quizResultRepository;
        private readonly IApplicationRepository applicationRepository;

        public InsightService(IQuizResultRepository quizResultRepository, IApplicationRepository applicationRepository)
        {
            this.quizResultRepository = quizResultRepository;
            this.applicationRepository = applicationRepository;
        }

        public async ValueTask<ApplicationStatusInsightDto> GetCountOfApplicationStatusAsync()
        {
            var applications = applicationRepository.SelectAllApplications();

            var applicationStatusInsightDto = new ApplicationStatusInsightDto();

            applicationStatusInsightDto.SentCount = applications.Where(x => x.Status == ApplicationStatus.Yuborildi).Count();
            applicationStatusInsightDto.PendingCount = applications.Where(x => x.Status == ApplicationStatus.Tekshirilmoqda).Count();
            applicationStatusInsightDto.WaitingPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovKutilmoqda).Count();
            applicationStatusInsightDto.SuccessPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovQilindi).Count();
            applicationStatusInsightDto.ApprovedPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovTasdiqlandi).Count();
            applicationStatusInsightDto.ScheduledTestCount =applications.Where(x => x.Status == ApplicationStatus.TestBelgilandi).Count();
            applicationStatusInsightDto.StartedTestCount = applications.Where(x => x.Status == ApplicationStatus.TestBoshlandi).Count();
            applicationStatusInsightDto.CanceledCount =applications.Where(x => x.Status == ApplicationStatus.BekorQilindi).Count();
            applicationStatusInsightDto.CompletedCount = applications.Where(x => x.Status == ApplicationStatus.Yakunlandi).Count();
            
            return applicationStatusInsightDto;
        }

        public async ValueTask<ApplicationStatusInsightDto> GetCountOfApplicationStatusBySchoolIdAsync(int schoolId)
        {
            var applications = applicationRepository.SelectAllApplications().Where(p => p.SchoolId == schoolId);

            var applicationStatusInsightDto = new ApplicationStatusInsightDto();

            applicationStatusInsightDto.SentCount = applications.Where(x => x.Status == ApplicationStatus.Yuborildi).Count();
            applicationStatusInsightDto.PendingCount = applications.Where(x => x.Status == ApplicationStatus.Tekshirilmoqda).Count();
            applicationStatusInsightDto.WaitingPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovKutilmoqda).Count();
            applicationStatusInsightDto.SuccessPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovQilindi).Count();
            applicationStatusInsightDto.ApprovedPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovTasdiqlandi).Count();
            applicationStatusInsightDto.ScheduledTestCount =applications.Where(x => x.Status == ApplicationStatus.TestBelgilandi).Count();
            applicationStatusInsightDto.StartedTestCount = applications.Where(x => x.Status == ApplicationStatus.TestBoshlandi).Count();
            applicationStatusInsightDto.CanceledCount =applications.Where(x => x.Status == ApplicationStatus.BekorQilindi).Count();
            applicationStatusInsightDto.CompletedCount = applications.Where(x => x.Status == ApplicationStatus.Yakunlandi).Count();

            return applicationStatusInsightDto;
        }

        public async ValueTask<ApplicationStatusInsightDto> GetCountOfApplicationStatusByTeacherIdAsync(int teacherId)
        {
            var applications = applicationRepository.SelectAllApplications().Where(p => p.TeacherId == teacherId);

            var applicationStatusInsightDto = new ApplicationStatusInsightDto();

            applicationStatusInsightDto.SentCount = applications.Where(x => x.Status == ApplicationStatus.Yuborildi).Count();
            applicationStatusInsightDto.PendingCount = applications.Where(x => x.Status == ApplicationStatus.Tekshirilmoqda).Count();
            applicationStatusInsightDto.WaitingPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovKutilmoqda).Count();
            applicationStatusInsightDto.SuccessPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovQilindi).Count();
            applicationStatusInsightDto.ApprovedPaymentCount = applications.Where(x => x.Status == ApplicationStatus.TolovTasdiqlandi).Count();
            applicationStatusInsightDto.ScheduledTestCount =applications.Where(x => x.Status == ApplicationStatus.TestBelgilandi).Count();
            applicationStatusInsightDto.StartedTestCount = applications.Where(x => x.Status == ApplicationStatus.TestBoshlandi).Count();
            applicationStatusInsightDto.CanceledCount =applications.Where(x => x.Status == ApplicationStatus.BekorQilindi).Count();
            applicationStatusInsightDto.CompletedCount = applications.Where(x => x.Status == ApplicationStatus.Yakunlandi).Count();

            return applicationStatusInsightDto;
        }

        public async ValueTask<QuizTopScoreInsightDto> GetCountOfTopInQuizResultAsync(int quizId)
        {
            var query = this.quizResultRepository
                .SelectAllQuizResults()
                .Where(p => p.QuizId  == quizId);

            // get statistics of quiz results, seperate by good, normal and bad, return one dto with 3 properties
            var quizResultGoodsCount = query.Where(qr => qr.Score >= 80).Count();
            var quizResultNormalsCount = query.Where(qr => qr.Score >= 60 && qr.Score < 80).Count();
            var quizResultBadsCount = query.Where(qr => qr.Score < 60).Count();

            var quizTopScoreInsightDto = new QuizTopScoreInsightDto
            {
                QuizResultGoodsCount = quizResultGoodsCount,
                QuizResultNormalsCount = quizResultNormalsCount,
                QuizResultBadsCount = quizResultBadsCount
            };

            return quizTopScoreInsightDto;
        }
    }
}
