namespace RaptorStreet.Models
{
    public class ResumoPedido
    {
        public Cliente Cliente { get; set; }
        public List<Produto> Produtos { get; set; }
        public decimal Total { get; set; }
    }
}
