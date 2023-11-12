using MediatR;

namespace MiniQr.Application.Queries.ObterStatusCobrancas
{
    /// <summary>
    /// Representa a solicitação para obter status das cobranças.
    /// </summary>
    public class ObterStatusCobrancasQuery : IRequest<List<ObterStatusCobrancasResponse>>
    {
        /// <summary>
        /// Obtém ou define o identificador único do usuário.
        /// </summary>
        public Guid IdUsuario { get; set; }
    }
}
