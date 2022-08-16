using AutoMapper;
using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Teacher, UserForCreationDto>().ReverseMap();   
    }
}