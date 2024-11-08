﻿using Sistema.Core.Dominio.DTO.Pessoa;
using Sistema.Core.Dominio.Models;

namespace Sistema.Core.Dominio.DTO.Turma
{
    public class TurmaDTO
    {
        public int Id { get; set; }
        public int IdProfessor { get; set; }

        public string NomeTurma { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;

        public virtual PessoaDTO Professor { get; set; }
        public virtual List<PessoaDTO> Alunos { get; set; }



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