
namespace ControleEstoque.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }         
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
