using AutoMapper;
using ApiTpEncode.Models;
using ApiTpEncode.Models.DTOs;
namespace ApiTpEncode.Servicios.AutoMaper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario,UsuarioGetDTO>();
            CreateMap<UsuarioPostDTO, Usuario>();
            CreateMap<UsuarioPutDTO, Usuario>();
        }

    }
}
