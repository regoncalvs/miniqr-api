using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniQr.Application.Commands.CriarUsuario;
using MiniQr.Application.Commands.LoginUsuario;
using MiniQr.Application.Commands.LogoutUsuario;
using MiniQr.Utils.Constants;
using MiniQr.Utils.Exceptions;

namespace MiniQr.API.Controllers
{
    /// <summary>
    /// Define o controller de Usuários
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsuariosController> _logger;

        /// <summary>
        /// Construtor do controller de Usuarios
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger">Logger</param>
        public UsuariosController(IMediator mediator, ILogger<UsuariosController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Adiciona novo usuário
        /// </summary>
        /// <param name="command">Objeto com os campos necessários para criação de um usuário</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost("cadastro")]
        [Authorize(Roles = Perfis.Master)]
        public async Task<IActionResult> CadastraUsuario(CriarUsuarioCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Usuário cadastrado!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza login de usuário
        /// </summary>
        /// <param name="command">Objeto com os campos necessários para realizar login (email e senha)</param>
        /// <returns>Token</returns>
        /// <response code="200">Caso login seja feito com sucesso</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioCommand command)
        {
            try
            {
                var resultado = await _mediator.Send(command);
                return Ok(resultado);
            }
            catch (Exception ex) when (ex is UsuarioNaoExisteException || ex is CredenciaisInvalidasException)
            {
                return ValidationProblem(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error"
                );
            }
        }

        /// <summary>
        /// Realiza logout de usuário
        /// </summary>
        /// <returns>Token</returns>
        /// <response code="200">Caso logout seja feito com sucesso</response>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _mediator.Send(new LogoutUsuarioCommand());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error"
                );
            }
        }
    }
}
