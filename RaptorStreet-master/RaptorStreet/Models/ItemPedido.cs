using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RaptorStreet.Models
{
    public class ItemPedido
    {
        public int IdProdutoPedido { get; set; }
        public int Fk_IdPedido { get; set; }
        public int Fk_IdProduto { get; set; }
        public decimal PrecoUnitario { get; set; }

        public Pedido Pedidos { get; set;  }
        public Produto Produtos { get; set; }

    }
}
