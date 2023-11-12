using Microsoft.Extensions.Configuration;

namespace MiniQr.Domain.Services.IntegracaoExterna
{
    /// <summary>
    /// Serviço base para comunicação de movimentações de cobranças com sistemas externos.
    /// </summary>
    public abstract class ComunicaMovimentacaoCobrancaService
    {
        protected readonly HttpClient _httpClient;
        protected readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor da classe <see cref="ComunicaMovimentacaoCobrancaService"/>.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP para realizar as chamadas de integração externa.</param>
        /// <param name="configuration">Configurações de aplicação para obtenção de informações necessárias.</param>
        public ComunicaMovimentacaoCobrancaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Método abstrato para realizar a comunicação da movimentação de cobrança com um sistema externo.
        /// </summary>
        /// <param name="objectBody">Corpo da requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
        public abstract Task Comunicar(object objectBody, CancellationToken cancellationToken);
    }
}
