using Evendas.Data.Context;
using Evendas.Domain.Interfaces;

namespace Evendas.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EvendasContext _context;

        public UnitOfWork(EvendasContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
