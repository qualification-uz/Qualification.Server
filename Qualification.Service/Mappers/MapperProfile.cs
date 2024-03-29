using AutoMapper;
using Qualification.Domain.Entities;
using Qualification.Domain.Entities.Assets;
using Qualification.Domain.Entities.Payment;
using Qualification.Domain.Entities.Payments;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Quizes;
using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Payment;
using Qualification.Service.DTOs.Question;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<Student, StudentResultDto>().ReverseMap();
        CreateMap<School, SchoolDto>().ReverseMap();

        CreateMap<Application, ApplicationDto>().ReverseMap();
        CreateMap<Application, ApplicationForCreationDto>().ReverseMap();
        CreateMap<Application, ApplicationForUpdateDto>().ReverseMap();

        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<Payment, PaymentForCreationDto>().ReverseMap();
        CreateMap<Payment, PaymentDtoForTeacher>().ReverseMap();

        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<Group, GroupForCreationDto>().ReverseMap();

        CreateMap<Question, QuestionDto>()
            .ForMember(dto => dto.AssetUrls, src => src
                .MapFrom(dest => dest.Assets
                    .Select(asset => Path.Combine("Images", asset.Asset == null ? string.Empty : asset.Asset.Url))))
            .ForMember(dto => dto.AssetIds, src => src 
                .MapFrom(dest => dest.Assets
                    .Select(asset => asset.AssetId)));

        CreateMap<QuestionForCreationDto, Question>().ReverseMap();
        CreateMap<QuestionAnswerForCreationDto, QuestionAnswer>().ReverseMap();

        CreateMap<QuestionAnswer, QuestionAnswerDto>()
            .ForMember(dto => dto.AssetUrls, src => src
                .MapFrom(dest => dest.Assets
                    .Select(asset => Path.Combine("Images", asset.Asset == null ? string.Empty : asset.Asset.Url))))
            .ForMember(dto => dto.AssetIds, src => src 
                .MapFrom(dest => dest.Assets
                    .Select(asset => asset.AssetId)));

        CreateMap<Asset, AssetDto>().ReverseMap();

        CreateMap<TeacherFromErpDto, TeacherForCreationDto>().ReverseMap();

        CreateMap<PaymentRequestDto, PaymentRequest>().ReverseMap();
        CreateMap<PaymentRequestForCreationDto, PaymentRequest>().ReverseMap();
        CreateMap<PaymentRequestForUpdateDto, PaymentRequest>().ReverseMap();
        CreateMap<PaymentAssetDto, PaymentAsset>().ReverseMap();

        CreateMap<QuizDto, Quiz>().ReverseMap();
        CreateMap<QuizForCreationDto, Quiz>().ReverseMap();
        CreateMap<QuizForUpdateDto, Quiz>().ReverseMap();
        CreateMap<QuizForStudent, QuizForStudentCreationDto>().ReverseMap();
        CreateMap<QuizForStudent, QuizeForStudentDto>().ReverseMap();

        CreateMap<QuizQuestionDto, Question>().ReverseMap()
            .ForMember(dto => dto.AssetUrls, src => src
                .MapFrom(dest => dest.Assets
                    .Select(asset => Path.Combine("Images", asset.Asset == null ? string.Empty : asset.Asset.Url))));

        CreateMap<QuizOptionDto, QuestionAnswer>().ReverseMap()
            .ForMember(dto => dto.AssetUrls, src => src
                .MapFrom(dest => dest.Assets
                    .Select(asset => Path.Combine("Images", asset.Asset == null ? string.Empty : asset.Asset.Url))));

        CreateMap<QuizResultDto, QuizResult>().ReverseMap();
        CreateMap<SubmissionDto, Submission>().ReverseMap();
        CreateMap<SubmissionDto, SubmissionResult>().ReverseMap();
        CreateMap<SubmissionForStudentDto, Submission>().ReverseMap();
        CreateMap<SubmissionForStudentDto, SubmissionResult>().ReverseMap();
        CreateMap<SubmissionForCreationDto, Submission>().ReverseMap();
        CreateMap<SubmissionForStudentDto, SubmissionResult>().ReverseMap();
        CreateMap<SubmissionForStudentForCreationDto, SubmissionResult>().ReverseMap();
    }
}