using MiniQr.Domain.Models;

namespace MiniQr.Application.Commands.PagarCobranca
{
    /// <summary>
    /// Resposta para o comando de pagamento de cobrança.
    /// </summary>
    public record PagarCobrancaResponse(Cobranca Cobranca);
}
