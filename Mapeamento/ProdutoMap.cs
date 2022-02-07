using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleEstoque.Mapeamento
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.ProdutoId);
            builder.Property(p => p.Nome).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Quatidade).IsRequired();
            builder.Property(p => p.Preco).IsRequired();

            builder.HasOne(p => p.Categoria).WithMany(p => p.Produto).HasForeignKey(p => p.CategoriaId).IsRequired();

            builder.HasMany(p => p.Movimentacoes).WithOne(p => p.Produto);

            builder.ToTable("Produtos");
        }
    }
}
