using AutoMapper;
using MiniQr.Application.Commands.CriarUsuario;
using MiniQr.Domain.Models;

namespace MiniQr.Application.Profiles
{
    /// <summary>
    /// Define o mapeamento para a entidade Usuario
    /// </summary>
    public class UsuarioProfile : Profile
    {
        /// <summary>
        /// Define o construtor do UsuarioProfile
        /// </summary>
        public UsuarioProfile()
        {
            CreateMap<CriarUsuarioCommand, Usuario>()
              .ForMember(Usuario => Usuario.UserName, opt => opt.MapFrom(src => src.Email))
              .ForMember(Usuario => Usuario.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()));
        }
    }
}
