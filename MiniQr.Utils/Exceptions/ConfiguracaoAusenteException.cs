namespace MiniQr.Utils.Exceptions
{
    /// <summary>
    /// Exceção lançada quando uma configuração não foi feita.
    /// </summary>
    public class ConfiguracaoAusenteException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ConfiguracaoAusenteException"/>.
        /// </summary>
        /// <param name="nomeConfiguracao">Nome da configuração ausente.</param>
        public ConfiguracaoAusenteException(string nomeConfiguracao) : base($"{nomeConfiguracao} não foi definido.")
        {
        }
    }
}
