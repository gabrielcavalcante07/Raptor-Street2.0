namespace RaptorStreet.Models
{
    public class ItemCarrinho
    {
        public int IdItemCarrinho { get; set; }

        public int Fk_IdCarrinho { get; set; }
        public Carrinho Carrinho { get; set; }

        public int Fk_IdProduto { get; set; }
        public Produto Produto { get; set; }
    }

}
