using FluentValidation;
using MediatR;
using MiniQr.Domain.Models;
using MiniQr.Domain.Services.IntegracaoExterna;
using MiniQr.Persistence;
using MiniQr.Utils.Exceptions;

namespace MiniQr.Application.Commands.CriarCobranca
{
    /// <summary>
    /// Manipulador para o comando de criação de cobrança.
    /// </summary>
    public class CriarCobrancaHandler : IRequestHandler<CriarCobrancaCommand, CriarCobrancaResponse>
    {
        private readonly MiniQrContext _context;
        private readonly IValidator<CriarCobrancaCommand> _validator;
        private readonly ComunicaNovaCobrancaService _comunicaNovaCobrancaService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CriarCobrancaHandler"/>.
        /// </summary>
        public CriarCobrancaHandler(MiniQrContext context, IValidator<CriarCobrancaCommand> validator, ComunicaNovaCobrancaService comunicaNovaCobrancaService)
        {
            _context = context;
            _validator = validator;
            _comunicaNovaCobrancaService = comunicaNovaCobrancaService;
        }

        /// <summary>
        /// Manipula o comando para criar uma nova cobrança.
        /// </summary>
        public async Task<CriarCobrancaResponse> Handle(CriarCobrancaCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                throw new FalhaNaValidacaoException(result.Errors);
            }

            var novaCobranca = new Cobranca(request.Descricao!, request.Valor!.Value, request.IdUsuario!.ToString());
            _context.Cobrancas.Add(novaCobranca);

            var objeto = new { value = novaCobranca.Valor };
            await _comunicaNovaCobrancaService.Comunicar(objeto, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new CriarCobrancaResponse(novaCobranca);
        }
    }
}
