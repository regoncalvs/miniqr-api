using MediatR;
using MiniQr.Domain.Enums;
using MiniQr.Domain.Services.IntegracaoExterna;
using MiniQr.Persistence;
using MiniQr.Utils.Exceptions;

namespace MiniQr.Application.Commands.CancelarCobranca
{
    /// <summary>
    /// Manipulador para o comando de cancelamento de cobrança.
    /// </summary>
    public class CancelarCobrancaHandler : IRequestHandler<CancelarCobrancaCommand, CancelarCobrancaResponse>
    {
        private readonly MiniQrContext _context;
        private readonly ComunicaCancelamentoCobrancaService _comunicaCancelamentoCobrancaService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CancelarCobrancaHandler"/>.
        /// </summary>
        public CancelarCobrancaHandler(MiniQrContext context, ComunicaCancelamentoCobrancaService comunicaCancelamentoCobrancaService)
        {
            _context = context;
            _comunicaCancelamentoCobrancaService = comunicaCancelamentoCobrancaService;
        }

        /// <summary>
        /// Manipula o comando para cancelar uma cobrança.
        /// </summary>
        public async Task<CancelarCobrancaResponse> Handle(CancelarCobrancaCommand request, CancellationToken cancellationToken)
        {
            var cobranca = _context.Cobrancas.SingleOrDefault(c => c.Id == request.IdCobranca) ?? throw new RegistroNaoEncontradoException("Cobrança não encontrada");

            if (cobranca.Status != (char)StatusCobrancaEnum.Paga) throw new CobrancaNaoPodeSerCanceladaException($"Cobrança não possui status \"{StatusCobrancaEnum.Paga.ToString()}\"");

            cobranca.Status = (char)StatusCobrancaEnum.Cancelada;
            _context.Update(cobranca);
            await _context.SaveChangesAsync(cancellationToken);

            var objetoBody = new { id = request.IdCobranca };
            await _comunicaCancelamentoCobrancaService.Comunicar(objetoBody, cancellationToken);

            return new CancelarCobrancaResponse(cobranca);
        }
    }
}
