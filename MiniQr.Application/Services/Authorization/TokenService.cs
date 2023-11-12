using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniQr.Domain.Models;
using MiniQr.Utils.Constants;
using MiniQr.Utils.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniQr.Application.Services.Authorization
{
    /// <summary>
    /// Define service para gerar token JWT
    /// </summary>
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _userManager;

        /// <summary>
        /// Define construtor para TokenService
        /// </summary>
        /// <param name="configuration"></param>
        public TokenService(IConfiguration configuration, UserManager<Usuario> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        /// <summary>
        /// Gera token para o usuário informado
        /// </summary>
        /// <param name="usuario">Usuário</param>
        /// <returns>Token JWT</returns>
        public async Task<string> GenerateTokenAsync(Usuario usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);
            var role = roles.FirstOrDefault();
            Claim[] claims = new Claim[]
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("role", role?? string.Empty)
            };
            var symmetricSecurityKey = _configuration[ConfiguracaoConstants.SymmetricSecurityKey] ?? throw new ConfiguracaoAusenteException(ConfiguracaoConstants.SymmetricSecurityKey);
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey));

            var signingCredentials =
                new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}