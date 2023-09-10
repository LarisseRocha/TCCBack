using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sdatcc_v2.Infrastructure.Entities
{
	public class TccEntity
	{
		[Key]
		public int Id { get; set; }
		public string Titulo { get; set; }
		public DateTime DataDefesa { get; set; }
		public string AreaEstudo { get; set; }
		public int ArquivoId { get; set; }
		public ArquivoEntity Arquivo { get; set; }
		public int AlunoId { get; set; }
		public virtual AlunoEntity Aluno { get; set; }
		public int ProfessorId { get; set; }
		public virtual ProfessorEntity Professor { get; set; }
	}
}
