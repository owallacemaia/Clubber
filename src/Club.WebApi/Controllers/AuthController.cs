using AutoMapper;
using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Business.Services;
using Club.WebApi.Configuration;
using Club.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Club.WebApi.Controllers
{
    [Route("api/contas")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public AuthController(INotificador notificador,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<AppSettings> appSettings,
            IUser user,
            IMapper mapper,
            IUsuarioService usuarioService,
            IUsuarioRepository usuarioRepository) : base(notificador, user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.NomeUsuario,
                Email = registerUser.Email,
                EmailConfirmed = true,
                PhoneNumber = registerUser.Celular,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                var usuario = new Usuario(Guid.Parse(user.Id), registerUser.NomeUsuario, registerUser.Nome, registerUser.DataNascimento, registerUser.Celular);
                await _usuarioService.Adicionar(usuario);
            }

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GerarJwt(user.UserName));
            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.NomeUsuario, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(loginUser.NomeUsuario));
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado!");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou senha incorretos");
            return CustomResponse();
        }

        [Authorize]
        [HttpGet("obter-usuario/{id:guid}")]
        public async Task<ActionResult<UsuarioViewModel>> ObterUsuarioPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGrupos(id);

            if (usuario.Id != id)
            {
                NotificarErro("O Id do usuário informado esta errado!");
                return CustomResponse();
            }

            var usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuario);

            return CustomResponse(usuarioViewModel);
        }

        [Authorize]
        [HttpPut("atualizar-usuario/{id:guid}")]
        public async Task<ActionResult<UsuarioViewModel>> AtualizarUsuario(Guid id, UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(usuarioViewModel);

            var usuario = await _usuarioRepository.ObterPorId(id);
            var usuarioIdentity = await _userManager.FindByIdAsync(id.ToString());
            if (usuario.Id != usuarioViewModel.Id)
            {
                NotificarErro("Usuario não esta correto");
                return CustomResponse();
            }

            var result = await _userManager.SetUserNameAsync(usuarioIdentity, usuarioViewModel.NomeUsuario);

            if (result.Succeeded)
            {
                await _usuarioService.Atualizar(_mapper.Map<Usuario>(usuarioViewModel));
            }

            return CustomResponse(usuarioViewModel);
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByNameAsync(email.ToUpper());
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.UserName.ToUpper()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToekn = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToekn,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
