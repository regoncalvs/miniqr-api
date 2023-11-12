using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniQr.Persistence;

namespace MiniQr.Application.Queries.ObterCobrancasPagas
{
    /// <summary>
    /// Manipula a solicitação para obter status das cobranças.
    /// </summary>
    public class ObterCobrancasHandler : IRequestHandler<ObterCobrancasQuery, List<ObterCobrancasResponse>>
    {
        private readonly MiniQrContext _context;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterCobrancasHandler"/>.
        /// </summary>
        /// <param name="context">O contexto da base de dados.</param>
        public ObterCobrancasHandler(MiniQrContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Manipula a solicitação para obter todas as cobranças pagas.
        /// </summary>
        /// <param name="request">A solicitação para obter as cobranças pagas.</param>
        /// <param name="cancellationToken">O token de cancelamento.</param>
        /// <returns>Um objeto de resposta com o funcionário obtido.</returns>
        public Task<List<ObterCobrancasResponse>> Handle(ObterCobrancasQuery request, CancellationToken cancellationToken)
        {
            var cobrancas = _context.Cobrancas
                .AsNoTracking()
                .Select(c => new ObterCobrancasResponse(c.Id.ToString(), c.Descricao, c.Valor, c.Status))
                .ToList();

            return Task.FromResult(cobrancas);
        }
    }
}
