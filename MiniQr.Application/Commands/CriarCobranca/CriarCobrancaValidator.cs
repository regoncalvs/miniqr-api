using FluentValidation;

namespace MiniQr.Application.Commands.CriarCobranca
{
    /// <summary>
    /// Validador para o comando de criação de cobrança.
    /// </summary>
    public class CriarCobrancaValidator : AbstractValidator<CriarCobrancaCommand>
    {
        public CriarCobrancaValidator()
        {
            RuleFor(r => r.Descricao)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informada!");
            RuleFor(r => r.Valor)
                .NotNull().WithMessage("{PropertyName} deve ser informado!")
                .GreaterThan(0).WithMessage("{PropertyName} deve ser maior que zero!");
            RuleFor(r => r.IdUsuario).NotEmpty();
        }
    }
}
