using MiniQr.Domain.Models;

namespace MiniQr.Application.Commands.CancelarCobranca
{
    /// <summary>
    /// Resposta para o comando de cancelamento de cobrança.
    /// </summary>
    public record CancelarCobrancaResponse(Cobranca Cobranca);
}
