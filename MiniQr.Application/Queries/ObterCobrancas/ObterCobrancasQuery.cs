using MediatR;

namespace MiniQr.Application.Queries.ObterCobrancasPagas
{
    /// <summary>
    /// Representa a solicitação para obter cobranças pagas.
    /// </summary>
    public class ObterCobrancasQuery : IRequest<List<ObterCobrancasResponse>>
    {       
    }
}
