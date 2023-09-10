using Microsoft.AspNetCore.Mvc;
using Sdatcc_v2.Domain;
using Sdatcc_v2.Infrastructure.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Sdatcc_v2.Infrastructure;

namespace Sdatcc_v2.Controllers
{
	[Route("api/interesse")]
	[ApiController]
	public class InteressePesquisaController : ControllerBase
	{
		private MyDbContext _myDbContext;

		public InteressePesquisaController(MyDbContext myDbContext)
		{
			//  _bd = bd;
			_myDbContext = myDbContext;
		}

		[HttpPost]
		public async Task<IActionResult> AdicionarInteresse([FromBody] InteressePesquisa interesse)
		{
			try
			{
				if (interesse == null)
				{
					return BadRequest("Dados de interesse inválidos.");
				}

				var interesseEntity = new InteressePesquisaEntity
				{
					Nome = interesse.Nome,
					Email = interesse.Email,
					assunto = interesse.Assunto
				};

				_myDbContext.InteressePesquisa.Add(interesseEntity);
				await _myDbContext.SaveChangesAsync();

				return Ok("Interesse registrado com sucesso.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Erro ao adicionar interesse: {ex.Message}");
			}
		}

		[HttpGet]
		public IActionResult ListarInteresses()
		{
			try
			{
				var interesses = _myDbContext.InteressePesquisa.ToList();
				return Ok(interesses);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Erro ao listar interesses: {ex.Message}");
			}
		}
	}
}