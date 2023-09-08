using Sdatcc_v2.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Sdatcc_v2.Infrastructure.Services
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this AlunoEntity paramAluno) 
        {
            List<Claim> result;
            result = new List<Claim>
            {
                    new(ClaimTypes.NameIdentifier, Convert.ToString(paramAluno.Id)),
                    new(ClaimTypes.Name, paramAluno.Nome),
                    new(ClaimTypes.Email, paramAluno.Email),
                    new(ClaimTypes.Role, "aluno")
             };
            return result;
        }

        public static IEnumerable<Claim> GetClaims(this ProfessorEntity paramProfessor)
        {            
            List<Claim> result;
            result = new List<Claim>
            {
                    new(ClaimTypes.NameIdentifier, Convert.ToString(paramProfessor.Id)),
                    new(ClaimTypes.Name, paramProfessor.Nome),
                    new(ClaimTypes.Email, paramProfessor.Email),
                    new(ClaimTypes.Role, "professor")
             };
            return result;
        }
    }
}
