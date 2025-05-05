namespace RaptorStreet.Models
{
    public class Carrinho
    {
        public int IdCarrinho { get; set; }
        public string TipoProduto { get; set; }
        public int TotalVenda { get; set; }
        public int Qtd { get; set; }
        public DateTime? DataVenda { get; set; }
        public int Fk_IdCliente { get; set; }

        public Cliente Clientes { get; set; }
    }
}
