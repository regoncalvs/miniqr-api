using FluentValidation;

namespace MiniQr.Application.Commands.CriarUsuario
{
    /// <summary>
    /// Validador para o comando de criação de usuário.
    /// </summary>
    public class CriarUsuarioValidator : AbstractValidator<CriarUsuarioCommand>
    {
        public CriarUsuarioValidator()
        {
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.Role).NotEmpty();
        }
    }
}
