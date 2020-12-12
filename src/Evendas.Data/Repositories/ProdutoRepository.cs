using Evendas.Data.Context;
using Evendas.Domain.Interfaces;
using Evendas.Domain.Models;

namespace Evendas.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(EvendasContext context)
            : base(context)
        {

        }
    }
}
