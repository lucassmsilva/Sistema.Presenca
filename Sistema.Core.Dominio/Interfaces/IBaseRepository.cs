using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Interfaces
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> Get(int id, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
    }
}
