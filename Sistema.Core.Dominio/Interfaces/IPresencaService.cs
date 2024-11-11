using Sistema.Core.Dominio.DTO.Presenca;
using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Interfaces
{
    public interface IPresencaService<T> where T : IPresenca
    {
        Task RegistrarPresenca(IEnumerable<T> lista);
        Task RegistrarPresenca(T item);
        Task CancelarPresenca(IEnumerable<T> lista);
        Task CancelarPresenca(T item);
        Task<IEnumerable<T>> ObterRegistrosPresenca(int idTurma);
    }
}
