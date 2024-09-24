using AutoMapper;
using Market.Auth.Domain.Models;

namespace Market.Auth.Application.Services.UserServices;

public class UserAutoMapperProfile : Profile
{
    public UserAutoMapperProfile()
    {
        CreateMap<UserBaseDto, User>();
    }
}
