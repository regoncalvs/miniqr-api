using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniQr.Persistence;

namespace MiniQr.Application.Queries.ObterStatusCobrancas
{
    /// <summary>
    /// Manipula a solicitação para obter status das cobranças.
    /// </summary>
    public class ObterStatusCobrancasHandler : IRequestHandler<ObterStatusCobrancasQuery, List<ObterStatusCobrancasResponse>>
    {
        private readonly MiniQrContext _context;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterStatusCobrancasHandler"/>.
        /// </summary>
        /// <param name="context">O contexto da base de dados.</param>
        public ObterStatusCobrancasHandler(MiniQrContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Manipula a solicitação para obter status das cobranças.
        /// </summary>
        /// <param name="request">A solicitação para obter status das cobranças.</param>
        /// <param name="cancellationToken">O token de cancelamento.</param>
        /// <returns>Um objeto de resposta com o funcionário obtido.</returns>
        public Task<List<ObterStatusCobrancasResponse>> Handle(ObterStatusCobrancasQuery request, CancellationToken cancellationToken)
        {
            var cobrancas = _context.Cobrancas
                .AsNoTracking()
                .Where(c => c.UsuarioId == request.IdUsuario.ToString())
                .Select(c => new ObterStatusCobrancasResponse(c.Id.ToString(), c.Descricao, c.Status))
                .ToList();

            return Task.FromResult(cobrancas);
        }
    }
}
