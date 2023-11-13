using MediatR;

namespace MiniQr.Application.Commands.PagarCobranca
{
    /// <summary>
    /// Comando para pagar uma cobrança.
    /// </summary>
    public class PagarCobrancaCommand : IRequest<PagarCobrancaResponse>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PagarCobrancaCommand"/>.
        /// </summary>
        public PagarCobrancaCommand(
            Guid idCobranca)
        {
            IdCobranca = idCobranca;
        }

        /// <summary>
        /// Obtém ou define o Id da cobrança.
        /// </summary>
        public Guid IdCobranca { get; set; }

    }
}
