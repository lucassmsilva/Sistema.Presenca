using Microsoft.EntityFrameworkCore;

using Sistema.Core.Dominio.Interfaces;
using Sistema.Infraestrutura.Persistencia.Context;

using System.Linq.Expressions;

namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext Context;

        public BaseRepository(AppDbContext context)
        {
            Context = context;
        }
        public void Create(T entity)
        {
            entity.DateCreated = DateTimeOffset.UtcNow;
            Context.Add(entity);

        }
        public void Update(T entity)
        {
            entity.DateUpdated = DateTimeOffset.UtcNow;
            Context.Update(entity);
        }


        public void Delete(T entity)
        {
            entity.DateDeleted = DateTimeOffset.UtcNow;
            Context.Remove(entity);
        }

        public async Task<T> Get(int id, CancellationToken cancellationToken)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await Context.Set<T>().ToListAsync(cancellationToken);
        }


        public virtual async Task<List<T>> Search(Expression<Func<T, bool>> filter, CancellationToken cancellationToken) =>
            await Context.Set<T>().Where(filter).ToListAsync(cancellationToken);
    }

}