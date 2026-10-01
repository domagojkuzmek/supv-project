using AutoMapper;
using Supv.Src.Supv.Contracts;

namespace Supv.Src.Supv.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<Vehicle, VehicleDTO>();
    }
}
