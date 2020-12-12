using Evendas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evendas.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(c => c.CodProduto)
                //.HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();


            builder.Property(c => c.Nome)
                //.HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Preco)
            //.HasColumnType("decimal(10,2)")
            .HasPrecision(10, 2)
            .IsRequired();
        }
    }
}
