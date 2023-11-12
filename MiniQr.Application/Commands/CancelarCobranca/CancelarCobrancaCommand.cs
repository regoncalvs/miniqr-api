using MediatR;

namespace MiniQr.Application.Commands.CancelarCobranca
{
    /// <summary>
    /// Comando para cancelar uma cobrança.
    /// </summary>
    public class CancelarCobrancaCommand : IRequest<CancelarCobrancaResponse>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CancelarCobrancaCommand"/>.
        /// </summary>
        public CancelarCobrancaCommand(
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
