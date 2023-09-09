using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Sdatcc_v2.Domain;
using Sdatcc_v2.Infrastructure;
using Sdatcc_v2.Infrastructure.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Sdatcc_v2.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sdatcc_v2.Controllers
{

	//[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class LoginController : ControllerBase
	{
		private MyDbContext _myDbContext;
		private readonly ILoginService _loginService;
		private readonly LoginEntity _login;
		public LoginController(MyDbContext myDbContext, ILoginService loginService)
		{

			_myDbContext = myDbContext;
			_loginService = loginService;

		}


		[AllowAnonymous]
		[Route("authenticate")]
		[HttpPost]
		public JsonResult Login([FromBody] LoginDto user)
		{
			var token = _loginService.Authenticate(user.Cpf, user.Senha);
			return new JsonResult(token);
		}

		[HttpPost]
		public bool ValidateJwtToken(string token)
		{
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes("xLarisseBatistadeMeloRochax");
				var tokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false, // Defina como true se desejar validar o emissor (issuer) do token
					ValidateAudience = false, // Defina como true se desejar validar a audiência (audience) do token
					ValidateLifetime = true, // Verifique se o token ainda é válido em termos de tempo
					IssuerSigningKey = new SymmetricSecurityKey(key)
				};

				SecurityToken validatedToken;
				var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

				// Verifique se o token não está expirado
				if (validatedToken.ValidTo < DateTime.UtcNow)
				{
					return false;
				}

				return true;
			}
			catch (Exception)
			{
				// Se ocorrer uma exceção, o token é inválido
				return false;
			}
		}

	}

	


	public class LoginDto
	{
		public string Cpf { get; set; }
		public string Senha { get; set; }
	}
}
