using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.Xml;

namespace RaptorStreet.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int Qtd { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public bool Desconto { get; set; }
        public int Tamanho { get; set; }
        public int Fk_IdMarca { get; set; }
        public MarcaProduto MarcaProdutos { get; set; }
        public ICollection<ClienteFav> ClienteFavs { get; set; }
        public ICollection<ItemPedido> ItemPedidos { get; set; }
    }
}
