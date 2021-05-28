using AutoMapper;
using Club.Business.Models;
using Club.WebApi.ViewModels;

namespace Club.WebApi.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<PostFeed, PostFeedViewModel>().ReverseMap();
            CreateMap<Grupo, GrupoViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioInfoViewModel>().ReverseMap();
        }
    }
}
