using AutoMapper;
using Qualification.Domain.Entities;
using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs.Application;
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
    }
}