using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdatcc_v2.Domain
{
	public class ConsultarTccCommand
	{
		public string AreaEstudo { get; set; }
		public string NomeAluno { get; set; }
		public string NomeProfessor { get; set; }
	}
}
