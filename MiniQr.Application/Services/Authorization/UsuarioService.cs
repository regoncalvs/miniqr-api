using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniQr.Application.Commands.CriarUsuario;
using MiniQr.Application.Commands.LoginUsuario;
using MiniQr.Domain.Models;
using MiniQr.Utils.Constants;
using MiniQr.Utils.Exceptions;

namespace MiniQr.Application.Services.Authorization
{
    /// <summary>
    /// Define service para gerenciamento de usuários
    /// </summary>
    public class UsuarioService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;

        /// <summary>
        /// Define construtor para UsuarioService
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="tokenService"></param>
        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userManager.UserValidators.Add(new UserValidator<Usuario>());
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="dto">Dto de criação de usuário</param>
        public async Task CadastraUsuario(CriarUsuarioCommand dto)
        {
            if (!await _roleManager.RoleExistsAsync(Perfis.Master)) await _roleManager.CreateAsync(new IdentityRole(Perfis.Master));
            if (!await _roleManager.RoleExistsAsync(Perfis.Administrador)) await _roleManager.CreateAsync(new IdentityRole(Perfis.Administrador));
            if (!await _roleManager.RoleExistsAsync(Perfis.Lojista)) await _roleManager.CreateAsync(new IdentityRole(Perfis.Lojista));

            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (!resultado.Succeeded)
            {
                throw new($"Falha ao cadastrar usuário! {resultado.Errors.FirstOrDefault()?.Description}");
            }

            await _userManager.AddToRoleAsync(usuario, dto.Role);
        }

        /// <summary>
        /// Realiza o login para o usuário informado
        /// </summary>
        /// <param name="dto">Dto de login de usuário</param>
        /// <returns>Token JWT</returns>
        public async Task<string> Login(LoginUsuarioCommand dto)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == dto.Email) ?? throw new UsuarioNaoExisteException();

            var resultado = await _signInManager.PasswordSignInAsync(dto.Email, dto.Senha, false, false);

            if (!resultado.Succeeded)
            {
                throw new CredenciaisInvalidasException();
            }

            var token = await _tokenService.GenerateTokenAsync(usuario);

            return token;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
