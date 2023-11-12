namespace MiniQr.Utils.Exceptions
{
    /// <summary>
    /// Exceção lançada quando uma cobrança não pode ser cancelada.
    /// </summary>
    public class CobrancaNaoPodeSerCanceladaException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CobrancaNaoPodeSerCanceladaException"/>.
        /// </summary>
        /// <param name="message">A mensagem de erro.</param>
        public CobrancaNaoPodeSerCanceladaException(string message) : base(message)
        {
        }
    }
}
