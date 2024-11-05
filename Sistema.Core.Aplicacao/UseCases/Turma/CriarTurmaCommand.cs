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

        public TurmaModel ToTurma() => new TurmaModel
        {
            NomeTurma = NormalizeTurmaName(NomeTurma),
            IdProfessor = IdProfessor
        };

        public static string NormalizeTurmaName(string name)
        {
            return name.ToUpperInvariant()
                        .Normalize(NormalizationForm.FormD)
                        .Replace('\u0300', '\0')
                        .Replace('\u0301', '\0')
                        .Replace('\u0323', '\0');
        }
    }
}

