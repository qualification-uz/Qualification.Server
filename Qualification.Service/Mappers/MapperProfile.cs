using AutoMapper;
using Qualification.Domain.Entities;
using Qualification.Domain.Entities.Assets;
using Qualification.Domain.Entities.Payments;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Question;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
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
            .ForMember(dto => dto.AssetIds, src => src
                .MapFrom(dest => dest.Assets
                    .Select(asset => asset.AssetId)));

        CreateMap<QuestionForCreationDto, Question>().ReverseMap();
        CreateMap<QuestionAnswerForCreationDto, QuestionAnswer>().ReverseMap();

        CreateMap<QuestionAnswer, QuestionAnswerDto>()
            .ForMember(dto => dto.AssetIds, src => src
                .MapFrom(dest => dest.Assets
                    .Select(asset => asset.AssetId)));

        CreateMap<Asset, AssetDto>().ReverseMap();
    }
}