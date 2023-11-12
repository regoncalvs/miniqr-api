namespace MiniQr.Utils.Exceptions
{
    /// <summary>
    /// Exceção lançada quando um item não é encontrado.
    /// </summary>
    public class RegistroNaoEncontradoException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="RegistroNaoEncontradoException"/>.
        /// </summary>
        /// <param name="message">A mensagem de erro.</param>
        public RegistroNaoEncontradoException(string message) : base(message)
        {
        }
    }
}
