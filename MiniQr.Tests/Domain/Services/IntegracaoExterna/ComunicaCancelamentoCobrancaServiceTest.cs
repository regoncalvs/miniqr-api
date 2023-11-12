using Microsoft.Extensions.Configuration;
using MiniQr.Domain.Services.IntegracaoExterna;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;

namespace MiniQr.Tests.Domain.Services.IntegracaoExterna
{
    public class ComunicaCancelamentoCobrancaServiceTest
    {
        public static IEnumerable<object[]> DadosTeste()
        {
            yield return new object[]
            {
                new { id = Guid.NewGuid() }
            };
        }

        [Theory]
        [MemberData(nameof(DadosTeste))]
        public async Task ValidaComunicaCancelamentoCobrancaService_DeveRealizarChamadaParaApiExternaAsync(object objetoBody)
        {
            #region Arrange
            string url = "https://api/urlcancelamento";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("Conte�do de resposta aqui")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var configuration = new Mock<IConfiguration>();

            configuration.Setup(x => x["IntegracoesExternas:UrlCancelarCobranca"]).Returns(url);

            var json = System.Text.Json.JsonSerializer.Serialize(objetoBody);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            var service = new ComunicaCancelamentoCobrancaService(httpClient, configuration.Object);
            #endregion

            #region Act
            await service.Comunicar(objetoBody, new CancellationToken());
            #endregion

            #region Assert
            mockHttpMessageHandler.Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Post &&
                        req.RequestUri == new Uri(url)
                        && req.Content!.ReadAsStringAsync().Result.Contains(json)
                    ),
                    ItExpr.IsAny<CancellationToken>()
                );
            #endregion
        }
    }
}
