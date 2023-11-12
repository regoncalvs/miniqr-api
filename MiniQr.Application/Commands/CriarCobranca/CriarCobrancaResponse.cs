using MiniQr.Domain.Models;

namespace MiniQr.Application.Commands.CriarCobranca
{
    /// <summary>
    /// Resposta para o comando de criação de cobrança.
    /// </summary>
    public record CriarCobrancaResponse(Cobranca Cobranca);
}
