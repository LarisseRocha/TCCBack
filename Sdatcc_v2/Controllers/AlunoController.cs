using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Sdatcc_v2.Domain;
using Sdatcc_v2.Infrastructure;
using Sdatcc_v2.Infrastructure.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sdatcc_v2.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        
        private MyDbContext _myDbContext;

        public AlunoController (MyDbContext myDbContext)
        {
           
            _myDbContext = myDbContext;

        }
        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            var dados = _myDbContext.Alunos;

            return Ok(dados);
        }

        // GET api/<AlunoController>/5
        [HttpGet("{Cpf}")]
        public IActionResult BuscarAlunoCpf(string Cpf)
        {
            var aluno = _myDbContext.Alunos.FirstOrDefault(c => c.Cpf == Cpf);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public async Task<IActionResult> CadastrarAluno([FromBody] Aluno value)
        {             

            AlunoEntity alunoEntity = new AlunoEntity();
            alunoEntity.Nome = value.Nome;
            var testNomeVazio = Regex.Replace(alunoEntity.Nome, @"\s+", "");
            if (testNomeVazio == string.Empty)
            {
	            return StatusCode(500);
            }
            bool hasNumber = value.Nome.Any(char.IsDigit);
            if (hasNumber)
            {
	            Console.WriteLine("Nome inválido");
	            return StatusCode(500);
            }
           
            alunoEntity.Email = value.Email;
            alunoEntity.DataNascimento = value.DataNascimento;
            alunoEntity.NumeroMatricula = value.NumeroMatricula;
            alunoEntity.Senha = value.Senha;
            value.Senha = value.Senha;
            _myDbContext.Alunos.Add(alunoEntity);
             await _myDbContext.SaveChangesAsync();

            return Ok();

        }


        // PUT api/<AlunoController>/5
        [HttpPut("{Cpf}")]
        public IActionResult AtualizarAluno(string Cpf, [FromBody] Alunov2 value)
        {
            var aluno = _myDbContext.Alunos.FirstOrDefault(c => c.Cpf == Cpf);
            if (aluno == null)
            {
                return BadRequest();
            }
            
            aluno.Nome = value.Nome;
            aluno.Email = value.Email;
            
            return Ok();
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{Id}")]
        public IActionResult ExcluirAluno(int Id)
        {
            var aluno = _myDbContext.Alunos.FirstOrDefault(c => c.Id == Id);

            if (aluno == null)
            {
                return NotFound();
            }

            _myDbContext.Alunos.Remove(aluno);

            return NoContent();
        }
    }
}
