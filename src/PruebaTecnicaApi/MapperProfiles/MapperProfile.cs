using Application.Contracts.Auth;
using Application.Contracts.Posts;
using AutoMapper;
using Domain.Entities;

namespace PruebaTecnicaApi.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            CreateMap<User, CreateUserRequest>().ReverseMap();
            CreateMap<User, CreateUserResponse>().ReverseMap();
            CreateMap<CreateUserResponse, CreateUserRequest>().ReverseMap();
            CreateMap<CreatePostRequest, PostResponse>().ReverseMap();
        }
    }
}
