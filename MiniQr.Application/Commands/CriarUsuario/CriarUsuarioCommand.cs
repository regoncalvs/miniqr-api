using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MiniQr.Application.Commands.CriarUsuario
{
    /// <summary>
    /// Define o comando para criar um novo usuário
    /// </summary>
    public class CriarUsuarioCommand : IRequest
    {
        /// <summary>
        /// Define o construtor do CriarUsuarioCommand
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="rePassword"></param>
        /// <param name="role"></param>
        public CriarUsuarioCommand(string nome, string email, string password, string rePassword, string role)
        {
            Nome = nome;
            Email = email;
            Password = password;
            RePassword = rePassword;
            Role = role;
        }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Senha do usuário para validação
        /// </summary>
        [Required]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        /// <summary>
        /// Role do usuário
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}
