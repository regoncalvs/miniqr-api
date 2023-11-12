using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniQr.Domain.Models;
using MiniQr.Utils.Constants;
using MiniQr.Utils.Exceptions;

namespace MiniQr.Persistence
{
    /// <summary>
    /// Inicializador da aplicação para configuração inicial do banco de dados.
    /// </summary>
    public static class ApplicationDbInitializer
    {
        /// <summary>
        /// Cria o usuário master no banco de dados.
        /// </summary>
        /// <param name="serviceProvider">Provedor de serviços.</param>
        public static async Task SeedUserMasterAsync(IServiceProvider serviceProvider)
        {
            using (UserManager<Usuario> userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>())
            {
                RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

                if (!await roleManager.RoleExistsAsync(Perfis.Master)) await roleManager.CreateAsync(new IdentityRole(Perfis.Master));

                var email = configuration[ConfiguracaoConstants.EmailMaster] ?? throw new ConfiguracaoAusenteException(ConfiguracaoConstants.EmailMaster);
                var senha = configuration[ConfiguracaoConstants.SenhaMaster] ?? throw new ConfiguracaoAusenteException(ConfiguracaoConstants.SenhaMaster);

                if (userManager.FindByEmailAsync(email).Result == null)
                {
                    Usuario user = new(nome: "Usuario Master")
                    {
                        UserName = email,
                        Email = email,
                    };

                    IdentityResult result = userManager.CreateAsync(user, senha).Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, Perfis.Master).Wait();
                    }
                }
            }
        }
    }
}
