namespace MiniQr.Domain.Enums
{
    /// <summary>
    /// Representa os diferentes estados de uma cobrança no sistema.
    /// </summary>
    public enum StatusCobrancaEnum
    {
        /// <summary>
        /// Cobrança no estado inicial, indicando que está nova.
        /// </summary>
        Nova = 'N',

        /// <summary>
        /// Cobrança que foi marcada como paga.
        /// </summary>
        Paga = 'P',

        /// <summary>
        /// Cobrança que foi cancelada.
        /// </summary>
        Cancelada = 'C'
    }
}
