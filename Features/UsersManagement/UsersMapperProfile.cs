using AutoMapper;
using Logbook.Entities;

namespace Logbook.Features.UsersManagement;

public class UsersMapperProfile : Profile
{
    public UsersMapperProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}