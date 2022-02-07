using ControleEstoque.Mapeamento;
using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;


namespace ControleEstoque.Connection
{
    public class ContextDB : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; }
        public ContextDB () : base() { }

        public ContextDB(DbContextOptions<ContextDB> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CategoriaMap());

            builder.ApplyConfiguration(new ProdutoMap());
            
            builder.ApplyConfiguration(new MovimentacaoMap());
        }
    }
}
