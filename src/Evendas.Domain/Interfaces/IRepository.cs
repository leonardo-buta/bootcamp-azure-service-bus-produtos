using System;
using System.Linq;
using System.Threading.Tasks;

namespace Evendas.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(long id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        Task RemoveAsync(long id);
        Task<int> SaveChangesAsync();
    }
}
