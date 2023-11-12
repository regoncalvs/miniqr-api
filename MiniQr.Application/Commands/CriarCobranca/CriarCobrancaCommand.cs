using MediatR;

namespace MiniQr.Application.Commands.CriarCobranca
{
    /// <summary>
    /// Comando para criar uma nova cobrança.
    /// </summary>
    public class CriarCobrancaCommand : IRequest<CriarCobrancaResponse>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CriarCobrancaCommand"/>.
        /// </summary>
        public CriarCobrancaCommand(
            string descricao,
            decimal? valor)
        {
            Descricao = descricao;
            Valor = valor;
        }

        /// <summary>
        /// Obtém ou define a descrição da cobrança.
        /// </summary>
        public string? Descricao { get; set; }

        /// <summary>
        /// Obtém ou define o valor da cobrança.
        /// </summary>
        public decimal? Valor { get; set; }

        /// <summary>
        /// Obtém ou define o Id do usuário relacionado à cobrança.
        /// </summary>
        public string? IdUsuario { get; set; }
    }
}
