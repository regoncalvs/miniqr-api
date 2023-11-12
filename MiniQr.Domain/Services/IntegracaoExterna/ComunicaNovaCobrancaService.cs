using Microsoft.Extensions.Configuration;
using System.Text;

namespace MiniQr.Domain.Services.IntegracaoExterna
{
    /// <summary>
    /// Serviço para comunicar a criação de novas cobranças com sistemas externos.
    /// </summary>
    public class ComunicaNovaCobrancaService : ComunicaMovimentacaoCobrancaService
    {
        /// <summary>
        /// Construtor da classe <see cref="ComunicaNovaCobrancaService"/>.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP para realizar as chamadas de integração externa.</param>
        /// <param name="configuration">Configurações de aplicação para obtenção de informações necessárias.</param>
        public ComunicaNovaCobrancaService(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration) { }

        /// <summary>
        /// Realiza a comunicação da criação de novas cobranças com um sistema externo.
        /// </summary>
        /// <param name="objetoBody">Corpo da requisição.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
        public override async Task Comunicar(object objetoBody, CancellationToken cancellationToken)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(objetoBody);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var url = _configuration["IntegracoesExternas:UrlNovaCobranca"];

            await _httpClient.PostAsync(url, conteudo, cancellationToken);
        }
    }
}
