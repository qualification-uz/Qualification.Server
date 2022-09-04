using AutoMapper;
using Qualification.Domain.Entities;
using Qualification.Domain.Entities.Assets;
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

        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<Group, GroupForCreationDto>().ReverseMap();
        
        CreateMap<Question, QuestionDto>().ReverseMap();
        CreateMap<QuestionForCreationDto, Question>().ReverseMap();
        CreateMap<QuestionAsset, QuestionAssetDto>().ReverseMap();

        CreateMap<QuestionAnswerAsset, QuestionAssetDto>().ReverseMap();
        CreateMap<QuestionAnswerForCreationDto, QuestionAnswer>().ReverseMap();
        CreateMap<QuestionAnswer, QuestionAnswerDto>().ReverseMap();

        CreateMap<Asset, AssetDto>().ReverseMap();
    }
}