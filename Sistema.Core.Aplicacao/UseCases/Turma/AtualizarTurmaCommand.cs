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
        public TurmaModel MapToTurma(TurmaModel turma)
        {
            turma.IdProfessor = IdProfessor;
            return turma;
        }
    }


}
