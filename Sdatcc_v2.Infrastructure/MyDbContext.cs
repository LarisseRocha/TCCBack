using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sdatcc_v2.Domain;
using Sdatcc_v2.Infrastructure.Entities;

namespace Sdatcc_v2.Infrastructure
{
	public class MyDbContext : DbContext
    {
        public MyDbContext() : base()
        {

        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {   
                string cnnString = "Persist Security Info=False;Integrated Security=true;Initial Catalog=larisseDB;server=ASUSDEV; TrustServerCertificate=true";
                optionsBuilder.UseSqlServer(cnnString);
            }
        }

        public DbSet<AlunoEntity> Alunos { get; set; }
        public DbSet<ProfessorEntity> Professores { get; set; }
        public DbSet<TccEntity> Tccs { get; set; }
        public DbSet<ArquivoEntity> Arquivos { get; set; }
        public DbSet<LoginEntity> Logins { get; set; }
        public DbSet<InteressePesquisaEntity> InteressePesquisa { get; set; }

        public async Task<AlunoEntity> LoginAlunoAsync(LoginDto paramLoginDto)
        {
            return await Alunos.Where(u => u.Cpf.Equals(paramLoginDto.Cpf) && u.Senha.Equals(paramLoginDto.Senha)).FirstOrDefaultAsync();
        }

        public async Task<ProfessorEntity> LoginProfessorAsync(LoginDto paramLoginDto)
        {
            return await Professores.Where(u => u.Cpf.Equals(paramLoginDto.Cpf) && u.Senha.Equals(paramLoginDto.Senha)).FirstOrDefaultAsync();
        }

        public List<TccEntity> ConsultaTcc(ConsultarTccCommand command)
        {
	        var query = Tccs.AsQueryable();

	        if (!string.IsNullOrEmpty(command.AreaEstudo))
	        {
		        query = query.Where(tcc => tcc.AreaEstudo.Contains(command.AreaEstudo));
	        }

	        if (!string.IsNullOrEmpty(command.NomeAluno))
	        {
		        query = query.Include(tcc => tcc.Aluno).Where(tcc => tcc.Aluno.Nome.Contains(command.NomeAluno));
	        }

	        if (!string.IsNullOrEmpty(command.NomeProfessor))
	        {
				query = query.Include(tcc => tcc.Professor).Where(tcc => tcc.Professor.Nome.Contains(command.NomeProfessor));
			}

	        return query
                .Include(x => x.Aluno)
                .Include(x => x.Arquivo)
                .Include(x => x.Professor)
                .ToList();
        }

        
    }
}