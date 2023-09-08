﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Sdatcc_v2.Domain
{
   public class Aluno
    {
        public string Nome { get; set; }
        public int  NumeroMatricula { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }

    }
}
