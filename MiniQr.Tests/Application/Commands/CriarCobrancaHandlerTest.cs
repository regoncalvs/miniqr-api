using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniQr.Application.Commands.CriarCobranca;
using MiniQr.Domain.Models;
using MiniQr.Domain.Services.IntegracaoExterna;
using MiniQr.Persistence;
using MiniQr.Utils.Exceptions;
using Moq;

namespace MiniQr.Tests.Application.Commands
{
    public class CriarCobrancaHandlerTest
    {
        public static IEnumerable<object[]> DadosTesteSucesso()
        {
            yield return new object[]
            {
                "Cobrança1", 1000m, Guid.NewGuid().ToString()
            };

            yield return new object[]
            {
                "Cobrança2", 4238.42m, Guid.NewGuid().ToString()
            };
        }

        public static IEnumerable<object[]> DadosTesteFalha()
        {
            yield return new object[]
            {
                "Cobrança1", 0m
            };

            yield return new object[]
            {
                "Cobrança2", null
            };

            yield return new object[]
            {
                string.Empty, 100m
            };

            yield return new object[]
            {
                0, 4238.42m
            };
        }

        [Theory]
        [MemberData(nameof(DadosTesteSucesso))]
        public void ValidaCriarCobrancaHandler_DeveCriarCobrancaEFazerIntegracaoExterna(string descricao, decimal? valor, string idUsuario)
        {
            #region Arrange
            var validator = new Mock<IValidator<CriarCobrancaCommand>>();
            var context = new Mock<MiniQrContext>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var configuration = new Mock<IConfiguration>().Object;

            var service = new Mock<ComunicaNovaCobrancaService>(httpClient, configuration);
            var mockSet = new Mock<DbSet<Cobranca>>();

            context
                .Setup(c => c.Cobrancas).Returns(mockSet.Object);

            validator
                .Setup(v => v.Validate(It.IsAny<CriarCobrancaCommand>()))
                .Returns(new ValidationResult());

            var handler = new CriarCobrancaHandler(context.Object, validator.Object, service.Object);

            var command = new CriarCobrancaCommand(descricao, valor) { IdUsuario = idUsuario };
            #endregion

            #region Act
            var result = handler.Handle(command, new CancellationToken()).Result;
            #endregion

            #region Assert
            Assert.NotNull(result);
            validator.Verify(v => v.Validate(It.IsAny<CriarCobrancaCommand>()), Times.Once);
            service.Verify(x => x.Comunicar(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            #endregion
        }

        [Theory]
        [MemberData(nameof(DadosTesteFalha))]
        public async Task ValidaCriarCobrancaCommand_DeveLancarExcecaoQuandoValidacaoFalhar(string descricao, decimal? valor)
        {
            #region Arrange
            var validator = new Mock<IValidator<CriarCobrancaCommand>>();
            var context = new Mock<MiniQrContext>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var configuration = new Mock<IConfiguration>().Object;

            var service = new Mock<ComunicaNovaCobrancaService>(httpClient, configuration);

            var mockSet = new Mock<DbSet<Cobranca>>();

            context
                .Setup(c => c.Cobrancas).Returns(mockSet.Object);

            var errors = new List<ValidationFailure>
            {
                new ValidationFailure("Descricao", "Erro 1"),
                new ValidationFailure("Valor", "Erro 2")
            };

            validator
                .Setup(v => v.Validate(It.IsAny<CriarCobrancaCommand>()))
                .Returns(new ValidationResult(errors));

            var handler = new CriarCobrancaHandler(context.Object, validator.Object, service.Object);

            var command = new CriarCobrancaCommand(descricao, valor);
            #endregion

            #region Act
            var exception = await Record.ExceptionAsync(async () => await handler.Handle(command, CancellationToken.None));
            #endregion

            #region Assert
            Assert.IsType<FalhaNaValidacaoException>(exception);
            validator.Verify(v => v.Validate(It.IsAny<CriarCobrancaCommand>()), Times.Once);
            #endregion
        }
    }
}
