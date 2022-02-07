using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleEstoque.Mapeamento
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {

            builder.HasKey(c => c.CategoriaId);
            builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            
            builder.HasMany(c => c.Produto).WithOne(c => c.Categoria).IsRequired();

            builder.ToTable("Categorias");


            //builder
            //    .Property(c => c.Nome)
            //    .IsRequired(D)
        }
    }
}
