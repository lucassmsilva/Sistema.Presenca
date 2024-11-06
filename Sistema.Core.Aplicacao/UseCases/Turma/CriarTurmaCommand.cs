using Sistema.Core.Aplicacao.Utils;
using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistema.Core.Aplicacao.UseCases.Turma
{
    public class CriarTurmaCommand
    {
        public int IdProfessor { get; set; }

        public string NomeTurma { get; set; } = string.Empty;

        public string Sigla { get; set; }   

        public TurmaModel ToTurma() => new TurmaModel
        {
            NomeTurma = StringUtils.NormalizeName(NomeTurma),
            IdProfessor = IdProfessor,
            Sigla = StringUtils.NormalizeName(Sigla)
        };
    }
}

