using Azure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniQr.Application.Queries.ObterStatusCobrancas;
using MiniQr.Utils.Constants;
using MiniQr.Utils.Exceptions;
using MiniQr.Application.Commands.CancelarCobranca;
using MiniQr.Application.Commands.CriarCobranca;
using MiniQr.Application.Queries.ObterCobrancasPagas;

namespace MiniQr.API.Controllers
{
    /// <summary>
    /// Controlador para operações relacionadas a cobranças.
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class CobrancasController : ControllerBase
    {
        private readonly ILogger<CobrancasController> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor da classe <see cref="CobrancasController"/>.
        /// </summary>
        /// <param name="logger">Instância do logger.</param>
        /// <param name="mediator">Instância do mediator para processar comandos e consultas.</param>
        public CobrancasController(ILogger<CobrancasController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova cobrança.
        /// </summary>
        /// <param name="command">Comando de criação de cobrança.</param>
        /// <returns>ActionResult com o resultado da criação.</returns>
        [HttpPost]
        [Authorize(Roles = Perfis.Lojista)]
        public async Task<IActionResult> CriarCobranca(CriarCobrancaCommand command)
        {
            try
            {
                var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value
                    ?? throw new AuthenticationFailedException("Não foi possível obter o id do usuário.");
                command.IdUsuario = idUsuario;
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (FalhaNaValidacaoException ex)
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
        /// Obtém o status das cobranças do usuário autenticado.
        /// </summary>
        /// <returns>ActionResult com o resultado da consulta de status das cobranças.</returns>
        [HttpGet("lojista")]
        [Authorize(Roles = Perfis.Lojista)]
        public async Task<IActionResult> ObterStatusCobrancasAsync()
        {
            try
            {
                var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                var result = await _mediator.Send(new ObterStatusCobrancasQuery { IdUsuario = Guid.Parse(idUsuario!) });

                return Ok(result);
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
        /// Obtém todas as cobranças com status "Paga".
        /// </summary>
        /// <returns>ActionResult com o resultado da consulta de cobranças.</returns>
        [HttpGet()]
        [Authorize(Roles = Perfis.Administrador)]
        public async Task<IActionResult> ObterCobrancasAdmin()
        {
            try
            {
                
                var result = await _mediator.Send(new ObterCobrancasQuery());

                return Ok(result);
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
        /// Cancela uma cobrança.
        /// </summary>
        /// <param name="idCobranca">Id da cobrança.</param>
        /// <returns>ActionResult com o resultado do cancelamento.</returns>
        [HttpPost("{idCobranca}/cancelar")]
        [Authorize(Roles = Perfis.Administrador)]
        public async Task<IActionResult> CancelarCobranca(Guid idCobranca)
        {
            try
            {
                CancelarCobrancaCommand command = new(idCobranca);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (RegistroNaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CobrancaNaoPodeSerCanceladaException ex)
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
    }
}
