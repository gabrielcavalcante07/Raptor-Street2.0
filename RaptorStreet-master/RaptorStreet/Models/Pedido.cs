using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;

namespace RaptorStreet.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string NomeProduto { get; set; }
        public string PrecoProduto { get; set; }
        public string ImagemProduto { get; set; }
        public DateTime dataPed { get; set; }
        public decimal totalPedido {get; set;}
        public int Fk_IdEndereco { get; set; }
        public int Fk_IdPag { get; set; }
        public int Fk_IdCliente { get; set; }
        public Endereco Enderecos { get; set; }
        public Pagamento Pagamentos { get; set; }
        public Cliente Clientes { get; set; }
        public ICollection<ItemPedido> ItemPedidos { get; set; }

    }
}
