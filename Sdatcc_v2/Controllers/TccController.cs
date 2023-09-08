using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sdatcc_v2.Domain;
using Sdatcc_v2.Infrastructure;
using Sdatcc_v2.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sdatcc_v2.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class TccController : ControllerBase
    {
        
        private MyDbContext _myDbContext;

        public TccController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        // GET: api/<TccController>
        [HttpGet]
        public IActionResult Get()
        {
            var dados = _myDbContext.Tccs.Include(t => t.Arquivo);
          
            return Ok(dados);
        }

        [HttpPost("{ConsultarTcc}")]
        [AllowAnonymous]
		public IActionResult ConsultarTcc([FromBody] ConsultarTccCommand command)
        {
	        var listTcc = _myDbContext.ConsultaTcc(command);
	        if (listTcc == null)
	        {
		        return NotFound();
	        }
	        return Ok(listTcc);
        }


		// POST api/<TccController>
		[HttpPost]
        public async Task<IActionResult> CadastrarTcc([FromBody] Tcc value)
        {
            TccEntity tccEntity = new TccEntity();
            tccEntity.Titulo = value.Titulo;
            tccEntity.AreaEstudo = value.AreaEstudo;
			tccEntity.DataDefesa = value.DataDefesa;

			_myDbContext.Tccs.Add(tccEntity);
			await _myDbContext.SaveChangesAsync();

			return Ok();
        }

        // PUT api/<TccController>/5
        [HttpPut("{Id}")]
        public IActionResult AtualizarTCC(int Id, [FromBody] Tccv2 value)
        {
            var tcc = _myDbContext.Tccs.FirstOrDefault(c => c.Id == Id);

            if (tcc == null)
            {
                return BadRequest();
            }

            tcc.AreaEstudo = value.AreaEstudo;

            return Ok();
        }

        // DELETE api/<TccController>/5
        [HttpDelete("{id}")]
        public IActionResult ExcluirTcc(int Id)
        {
            var tcc = _myDbContext.Tccs.FirstOrDefault(c => c.Id == Id);
            if (tcc == null)
            {
                return NotFound();
            }

            _myDbContext.Tccs.Remove(tcc);
            return NoContent();
        }
    }
}
