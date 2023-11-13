namespace MiniQr.Utils.Exceptions
{
    /// <summary>
    /// Exceção lançada quando uma cobrança não pode ser paga.
    /// </summary>
    public class CobrancaNaoPodeSerPagaException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CobrancaNaoPodeSerPagaException"/>.
        /// </summary>
        /// <param name="message">A mensagem de erro.</param>
        public CobrancaNaoPodeSerPagaException(string message) : base(message)
        {
        }
    }
}
