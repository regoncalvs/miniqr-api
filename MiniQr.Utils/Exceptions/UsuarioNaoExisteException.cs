namespace MiniQr.Utils.Exceptions
{
    /// <summary>
    /// Exceção lançada quando um usuário não é encontrado no login
    /// </summary>
    public class UsuarioNaoExisteException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UsuarioNaoExisteException"/>.
        /// </summary>
        public UsuarioNaoExisteException() : base("Usuário não existe!")
        {
        }
    }
}
