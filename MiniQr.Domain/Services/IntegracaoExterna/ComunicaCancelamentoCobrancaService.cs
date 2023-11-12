using Microsoft.Extensions.Configuration;
using System.Text;

namespace MiniQr.Domain.Services.IntegracaoExterna
{
    /// <summary>
    /// Serviço para comunicar o cancelamento de cobranças com sistemas externos.
    /// </summary>
    public class ComunicaCancelamentoCobrancaService : ComunicaMovimentacaoCobrancaService
    {
        /// <summary>
        /// Construtor da classe <see cref="ComunicaCancelamentoCobrancaService"/>.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP para realizar as chamadas de integração externa.</param>
        /// <param name="configuration">Configurações de aplicação para obtenção de informações necessárias.</param>
        public ComunicaCancelamentoCobrancaService(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration) { }

        /// <summary>
        /// Realiza a comunicação do cancelamento de cobranças com um sistema externo.
        /// </summary>
        /// <param name="objetoBody">Corpo da requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
        public override async Task Comunicar(object objetoBody, CancellationToken cancellationToken)
        {            
            var json = System.Text.Json.JsonSerializer.Serialize(objetoBody);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var url = _configuration["IntegracoesExternas:UrlCancelarCobranca"];

            await _httpClient.PostAsync(url, conteudo, cancellationToken);
        }
    }
}
