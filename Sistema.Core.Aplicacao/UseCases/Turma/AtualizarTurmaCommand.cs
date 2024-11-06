using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.Turma
{
    public class AtualizarTurmaCommand
    {
        public int Id { get; set; }
        public int IdProfessor { get; set; }
        public string NomeTurma { get; set; } = string.Empty;

        public string Sigla { get; set; } = string.Empty;
        public TurmaModel MapToTurma(TurmaModel turma)
        {
            turma.IdProfessor = IdProfessor;
            turma.NomeTurma = NomeTurma;
            turma.Sigla = Sigla;
            return turma;
        }
    }


}
