using AutoMapper;
using Contracts;
using SearchService;

namespace SearchService;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserCreated , Item>();
        CreateMap<UserUpdated , Item>();
    }
}
