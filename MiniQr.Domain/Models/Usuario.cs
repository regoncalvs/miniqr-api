using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MiniQr.Domain.Models
{
  /// <summary>
  /// Define a entidade Usuario
  /// </summary>
  public class Usuario : IdentityUser
  {
    /// <summary>
    /// Define o construtor da entidade Usuario
    /// </summary>
    /// <param name="nome"></param>
    public Usuario(string nome) : base()
    {
      Nome = nome;
    }

    /// <summary>
    /// Obtém ou define o nome do usuário
    /// </summary>
    [Required]
    public string Nome { get; set; }
  }
}
