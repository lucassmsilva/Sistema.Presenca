using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Interfaces
{
    public interface IPresencaService
    {
        bool RegistrarPresenca<T>(IEnumerable<T> pessoas);
        bool RegistrarPresenca<T>(T pessoa);
        bool CancelarPresenca<T>(IEnumerable<T> pessoas);
        bool CancelarPresenca<T>(T pessoa);
        IEnumerable<Presenca> ObterRegistrosPresenca(DateTime dataInicial, DateTime dataFinal);
    }
}
