using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniQr.Domain.Models;

namespace MiniQr.Persistence
{
    /// <summary>
    /// Contexto para acesso aos dados.
    /// </summary>
    public class MiniQrContext : IdentityDbContext<Usuario>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="MiniQrContext"/>.
        /// </summary>
        public MiniQrContext()
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="MiniQrContext"/>.
        /// </summary>
        /// <param name="options">As opções de configuração do contexto.</param>
        /// <param name="logger">O logger para o contexto.</param>
        public MiniQrContext(
            DbContextOptions<MiniQrContext> options) : base(options)
        {
        }

        /// <summary>
        /// Obtém ou define as cobranças no contexto.
        /// </summary>
        public virtual DbSet<Cobranca> Cobrancas => Set<Cobranca>();

        /// <summary>
        /// Obtém ou define os usuários no contexto.
        /// </summary>
        public virtual DbSet<Usuario> Usuarios => Set<Usuario>();

        /// <summary>
        /// Configura o relacionamento entre usuários e cobranças e a tabela de usuário.
        /// </summary>
        /// <param name="modelBuilder">O construtor de modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(u =>
            {
                u.Property(u => u.Email).IsRequired();
            });

            modelBuilder.Entity<Cobranca>()
              .HasOne(c => c.Usuario)
              .WithMany()
              .HasForeignKey(t => t.UsuarioId);

            modelBuilder.Entity<Usuario>().Ignore(c => c.ConcurrencyStamp)
                                     .Ignore(c => c.PhoneNumber)
                                     .Ignore(c => c.PhoneNumberConfirmed)
                                     .Ignore(c => c.TwoFactorEnabled)
                                     .Ignore(c => c.LockoutEnd)
                                     .Ignore(c => c.LockoutEnabled)
                                     .Ignore(c => c.TwoFactorEnabled);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        }
    }
}