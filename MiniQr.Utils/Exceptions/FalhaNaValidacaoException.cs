using FluentValidation.Results;
using System.Text;

namespace MiniQr.Utils.Exceptions
{
    /// <summary>
    /// Exceção lançada quando a validação de um objeto falha.
    /// </summary>
    public class FalhaNaValidacaoException : Exception
    {
        /// <summary>
        /// Obtém os erros de validação que ocorreram.
        /// </summary>
        public List<ValidationFailure> ValidationErrors { get; }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="FalhaNaValidacaoException"/>.
        /// </summary>
        /// <param name="validationErrors">A lista de erros de validação.</param>
        public FalhaNaValidacaoException(List<ValidationFailure> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Obtém a mensagem de erro.
        /// </summary>
        public override string Message
        {
            get
            {
                var message = new StringBuilder("Falha na validação: ");
                foreach (var error in ValidationErrors)
                {
                    message.AppendLine($"{error.ErrorMessage} (Propriedade: {error.PropertyName})");
                }
                return message.ToString();
            }
        }
    }
}
