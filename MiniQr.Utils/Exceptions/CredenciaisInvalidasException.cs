namespace MiniQr.Utils.Exceptions
{
    /// <summary>
    /// Exceção lançada quando um usuário não é encontrado no login
    /// </summary>
    public class CredenciaisInvalidasException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CredenciaisInvalidasException"/>.
        /// </summary>
        /// <param name="nomeConfiguracao">Nome da configuração ausente.</param>
        public CredenciaisInvalidasException() : base("Credenciais inválidas!")
        {
        }
    }
}
