using Evendas.Data.Context;
using Evendas.Domain.Interfaces;
using System.Threading.Tasks;

namespace Evendas.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EvendasContext _context;

        public UnitOfWork(EvendasContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
