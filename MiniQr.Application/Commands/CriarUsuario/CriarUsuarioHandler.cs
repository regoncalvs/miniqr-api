using FluentValidation;
using MediatR;
using MiniQr.Application.Services.Authorization;
using MiniQr.Utils.Exceptions;

namespace MiniQr.Application.Commands.CriarUsuario
{
    /// <summary>
    /// Manipulador para o comando de criação de usuário.
    /// </summary>
    public class CriarUsuarioHandler : IRequestHandler<CriarUsuarioCommand>
    {
        private readonly IValidator<CriarUsuarioCommand> _validator;
        private readonly UsuarioService _usuarioService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CriarUsuarioHandler"/>.
        /// </summary>       
        public CriarUsuarioHandler(IValidator<CriarUsuarioCommand> validator, UsuarioService usuarioService)
        {
            _validator = validator;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Manipula o comando para criar um novo usuário.
        /// </summary>
        public async Task Handle(CriarUsuarioCommand command, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(command);
            if (!result.IsValid)
            {
                throw new FalhaNaValidacaoException(result.Errors);
            }

            await _usuarioService.CadastraUsuario(command);
        }
    }
}
