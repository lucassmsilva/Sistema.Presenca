﻿using Sistema.Core.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Models
{
    public class Turma : BaseEntity
    {
        public long IdProfessor { get; set; }

        public string NomeTurma { get; set; }

    }
        
}
