using Microsoft.Extensions.Configuration;

namespace MiniQr.Domain.Services.IntegracaoExterna
{
    /// <summary>
    /// Serviço para comunicar o pagamento de cobranças com sistemas externos.
    /// </summary>
    public class ComunicaPagamentoCobrancaService : ComunicaMovimentacaoCobrancaService
    {
        /// <summary>
        /// Construtor da classe <see cref="ComunicaPagamentoCobrancaService"/>.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP para realizar as chamadas de integração externa.</param>
        /// <param name="configuration">Configurações de aplicação para obtenção de informações necessárias.</param>
        public ComunicaPagamentoCobrancaService(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration) { }

        /// <summary>
        /// Realiza a comunicação do pagamento de cobranças com um sistema externo.
        /// </summary>
        /// <param name="objetoBody">Corpo da requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
        public override Task Comunicar(object objetoBody, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
