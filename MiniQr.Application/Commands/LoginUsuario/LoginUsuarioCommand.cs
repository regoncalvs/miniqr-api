using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MiniQr.Application.Commands.LoginUsuario
{
    /// <summary>
    /// Define o comando para realizar login
    /// </summary>
    public class LoginUsuarioCommand :IRequest<LoginUsuarioResponse>
    {
        /// <summary>
        /// Define o construtor do LoginUsuarioCommand
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        public LoginUsuarioCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required]
        public string Senha { get; set; }
    }
}
