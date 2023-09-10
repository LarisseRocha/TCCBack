using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdatcc_v2.Infrastructure.Services
{
	public interface ILoginService
	{
		string GerarToken(string cpf);
		string Authenticate(string userCpf, string userSenha);
	}

}
