using MediatR;
using MiniQr.Domain.Enums;
using MiniQr.Domain.Services.IntegracaoExterna;
using MiniQr.Persistence;
using MiniQr.Utils.Exceptions;

namespace MiniQr.Application.Commands.PagarCobranca
{
    /// <summary>
    /// Manipulador para o comando de pagamento de cobrança.
    /// </summary>
    public class PagarCobrancaHandler : IRequestHandler<PagarCobrancaCommand, PagarCobrancaResponse>
    {
        private readonly MiniQrContext _context;
        private readonly ComunicaPagamentoCobrancaService _comunicaPagamentoCobrancaService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PagarCobrancaHandler"/>.
        /// </summary>
        public PagarCobrancaHandler(MiniQrContext context, ComunicaPagamentoCobrancaService comunicaPagamentoCobrancaService)
        {
            _context = context;
            _comunicaPagamentoCobrancaService = comunicaPagamentoCobrancaService;
        }

        /// <summary>
        /// Manipula o comando para pagar uma cobrança.
        /// </summary>
        public async Task<PagarCobrancaResponse> Handle(PagarCobrancaCommand request, CancellationToken cancellationToken)
        {
            var cobranca = _context.Cobrancas.SingleOrDefault(c => c.Id == request.IdCobranca) ?? throw new RegistroNaoEncontradoException("Cobrança não encontrada");

            if (cobranca.Status != (char)StatusCobrancaEnum.Nova) throw new CobrancaNaoPodeSerPagaException($"Cobrança não possui status \"{StatusCobrancaEnum.Nova.ToString()}\"");

            cobranca.Status = (char)StatusCobrancaEnum.Paga;
            _context.Update(cobranca);
            await _context.SaveChangesAsync(cancellationToken);

            var objetoBody = new { id = request.IdCobranca };
            await _comunicaPagamentoCobrancaService.Comunicar(objetoBody, cancellationToken);

            return new PagarCobrancaResponse(cobranca);
        }
    }
}
