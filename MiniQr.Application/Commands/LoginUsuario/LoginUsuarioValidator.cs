using FluentValidation;

namespace MiniQr.Application.Commands.LoginUsuario
{
    /// <summary>
    /// Validador para o comando de login de usuário.
    /// </summary>
    public class LoginUsuarioValidator : AbstractValidator<LoginUsuarioCommand>
    {
        public LoginUsuarioValidator()
        {
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.Senha).NotEmpty();
        }
    }
}
