using Evendas.Data.Mappings;
using Evendas.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Evendas.Data.Context
{
    public class EvendasContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
