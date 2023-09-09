using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Sdatcc_v2.Infrastructure.Services
{
	public class LoginService : ILoginService
	{
		private readonly string _chaveSecreta = "sua-chave-secreta"; // Substitua pela sua chave secreta real
		private readonly double _tempoExpiracaoEmMinutos = 20; // Define o tempo de expiração do token (30 minutos no exemplo)


		public string Authenticate(string cpf, string password)
		{
			var user = "Larisse"; //_userRepository.GetUser(email, password);

			if (user == null) return null;

			var tokenHandler = new JwtSecurityTokenHandler();

			var tokenKey = Encoding.ASCII.GetBytes("xLarisseBatistadeMeloRochax");

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new(ClaimTypes.SerialNumber, cpf)
				}),

				SigningCredentials = new SigningCredentials
				(
					new SymmetricSecurityKey(tokenKey),
					SecurityAlgorithms.HmacSha256Signature
				)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		public string GerarToken(string usuario)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var chave = Encoding.ASCII.GetBytes(_chaveSecreta);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, usuario)
				}),
				Expires = DateTime.UtcNow.AddMinutes(_tempoExpiracaoEmMinutos),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
	

