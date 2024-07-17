using AutoMapper;
using TypistApi.DTOs;
using TypistApi.Entity;

namespace TypistApi.RequestHelper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User , UserDto>();
    }
}
