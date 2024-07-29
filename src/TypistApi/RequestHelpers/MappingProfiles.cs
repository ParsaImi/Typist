using AutoMapper;
using Contracts;
using TypistApi.DTOs;
using TypistApi.Entity;

namespace TypistApi.RequestHelper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User , UserDto>();
        CreateMap<User , UserAllDto>();
        CreateMap<UserAllDto , User>();
        CreateMap<UserAllDto , UserCreated>();
        CreateMap<User , UserUpdated>();
    }
}
