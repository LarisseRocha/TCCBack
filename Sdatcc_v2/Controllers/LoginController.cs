using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sdatcc_v2.Infrastructure;
using Sdatcc_v2.Infrastructure.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using Sdatcc_v2.Infrastructure.Services;
using Sdatcc_v2.Domain;
using Microsoft.Graph.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sdatcc_v2.Controllers
{

	//[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class LoginController : ControllerBase
	{
		private MyDbContext _myDbContext;
        private readonly TokenService _tokenService;
        
		public LoginController(MyDbContext myDbContext, TokenService tokenService)
		{
			_myDbContext = myDbContext;
            _tokenService = tokenService;
        }
		
		[HttpPost("authenticate")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginDto paramLoginDto)
		{
			var aluno = await _myDbContext.LoginAlunoAsync(paramLoginDto);
            var professor = await _myDbContext.LoginProfessorAsync(paramLoginDto);
			string token;
			if (aluno != null)
			{
				token = _tokenService.GenerateToken(aluno);
				return Ok(token);
			}
			else if (professor != null)
			{
                token = _tokenService.GenerateToken(professor);
				return Ok(token);
			}
			else 
			{
                return Unauthorized("");
            }
		}

		[HttpPost]
		public bool ValidateJwtToken(string token)
		{
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes("LarisseBatistadeMeeloRocha");
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
}
