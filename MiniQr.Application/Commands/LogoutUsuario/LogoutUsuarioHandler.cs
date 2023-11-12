using MediatR;
using MiniQr.Application.Services.Authorization;

namespace MiniQr.Application.Commands.LogoutUsuario
{
    /// <summary>
    /// Manipulador para o comando de logout de usuário.
    /// </summary>
    public class LogoutUsuarioHandler : IRequestHandler<LogoutUsuarioCommand>
    {
        private readonly UsuarioService _usuarioService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="LogoutUsuarioHandler"/>.
        /// </summary>       
        public LogoutUsuarioHandler(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Manipula o comando para fazer logout.
        /// </summary>
        public async Task Handle(LogoutUsuarioCommand command, CancellationToken cancellationToken)
        {
            await _usuarioService.Logout();
        }
    }
}
