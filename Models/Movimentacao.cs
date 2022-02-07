namespace ControleEstoque.Models
{
    public class Movimentacao
    {
        public int MovimentacaoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
    }
}
