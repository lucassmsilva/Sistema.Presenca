using Sistema.Core.Aplicacao.UseCases.Pessoa;
using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.Turma
{
    public class TurmaDTO
    {
        public int IdProfessor { get; set; }

        public string NomeTurma { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;


        public static TurmaDTO FromEntity(TurmaModel turma)
        {
            return new()
            {
                IdProfessor = turma.IdProfessor,
                NomeTurma = turma.NomeTurma,
                Sigla = turma.Sigla,    
            };
        }
    }



}
