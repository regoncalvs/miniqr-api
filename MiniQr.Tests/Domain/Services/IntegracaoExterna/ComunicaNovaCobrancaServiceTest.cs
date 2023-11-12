using Microsoft.Extensions.Configuration;
using MiniQr.Domain.Services.IntegracaoExterna;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;

namespace MiniQr.Tests.Domain.Services.IntegracaoExterna
{
    public class ComunicaNovaCobrancaServiceTest
    {
        public static IEnumerable<object[]> DadosTeste()
        {
            yield return new object[]
            {
                new { valor = 1000 }
            };

            yield return new object[]
            {
                new { valor = 1455.2m }
            };
        }

        [Theory]
        [MemberData(nameof(DadosTeste))]
        public async Task ValidaComunicaNovaCobrancaService_DeveRealizarChamadaParaApiExternaAsync(object objetoBody)
        {
            #region Arrange
            string url = "https://api/urlnovacobranca";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("Conteúdo de resposta aqui")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var configuration = new Mock<IConfiguration>();

            configuration.Setup(x => x["IntegracoesExternas:UrlNovaCobranca"]).Returns(url);

            var json = System.Text.Json.JsonSerializer.Serialize(objetoBody);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            var service = new ComunicaNovaCobrancaService(httpClient, configuration.Object);
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
