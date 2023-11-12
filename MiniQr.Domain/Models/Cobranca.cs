using MiniQr.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniQr.Domain.Models
{
    /// <summary>
    /// Representa uma cobrança no sistema.
    /// </summary>
    public class Cobranca
    {
        /// <summary>
        /// Construtor da classe Cobranca.
        /// </summary>
        /// <param name="descricao">Descrição da cobrança.</param>
        /// <param name="valor">Valor da cobrança.</param>
        /// <param name="usuarioId">ID do usuário associado à cobrança.</param>
        public Cobranca(string descricao, decimal valor, string usuarioId)
        {
            Descricao = descricao;
            Valor = valor;
            UsuarioId = usuarioId;
            Status = (char)StatusCobrancaEnum.Nova;
        }

        /// <summary>
        /// Obtém ou define o ID da cobrança.
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da cobrança.
        /// </summary>
        [Required]
        public string Descricao { get; set; }

        /// <summary>
        /// Obtém ou define o valor da cobrança.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }

        /// <summary>
        /// Obtém ou define o status da cobrança.
        /// </summary>
        [Required]
        public char Status { get; set; }

        /// <summary>
        /// Obtém ou define o ID do usuário associado à cobrança.
        /// </summary>
        [Required]
        public string UsuarioId { get; set; }

        /// <summary>
        /// Obtém ou define o usuário associado à cobrança.
        /// </summary>
        public virtual Usuario? Usuario { get; set; }
    }
}
