﻿using Sistema.Core.Dominio.Interfaces;

namespace Sistema.Core.Dominio.Models
{
    public class PresencaModel : BaseEntity
    {
        public int IdPessoa { get; set; }
        public int IdTurmaHorario { get; set; }
        public bool Presente { get; set; }

    }
}
