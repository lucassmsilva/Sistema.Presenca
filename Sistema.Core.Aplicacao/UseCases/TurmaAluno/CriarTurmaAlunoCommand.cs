using Sistema.Core.Aplicacao.Utils;
using Sistema.Core.Dominio.DTO.Pessoa;
using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.TurmaAluno
{
    public class CriarTurmaAlunoCommand
    {
        public int IdTurma { get; set; }

        public List<int> Pessoas { get; set; }
    }
}
