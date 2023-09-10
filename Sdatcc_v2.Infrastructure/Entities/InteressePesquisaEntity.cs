using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Sdatcc_v2.Domain;
using BCrypt;

namespace Sdatcc_v2.Infrastructure.Entities
{
	public class InteressePesquisaEntity
	{
		[Key]
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string assunto { get; set; }
	}
}