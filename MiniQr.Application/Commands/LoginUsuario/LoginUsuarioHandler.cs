using FluentValidation;
using MediatR;
using MiniQr.Application.Services.Authorization;
using MiniQr.Utils.Exceptions;

namespace MiniQr.Application.Commands.LoginUsuario
{
    /// <summary>
    /// Manipulador para o comando de login de usuário.
    /// </summary>
    public class LoginUsuarioHandler : IRequestHandler<LoginUsuarioCommand, LoginUsuarioResponse>
    {
        private readonly IValidator<LoginUsuarioCommand> _validator;
        private readonly UsuarioService _usuarioService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="LoginUsuarioHandler"/>.
        /// </summary>       
        public LoginUsuarioHandler(IValidator<LoginUsuarioCommand> validator, UsuarioService usuarioService)
        {
            _validator = validator;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Manipula o comando para criar um novo usuário.
        /// </summary>
        public async Task<LoginUsuarioResponse> Handle(LoginUsuarioCommand command, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(command);
            if (!result.IsValid)
            {
                throw new FalhaNaValidacaoException(result.Errors);
            }

            var token = await _usuarioService.Login(command);

            return new LoginUsuarioResponse(token);
        }
    }
}
