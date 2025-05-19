using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace RaptorStreet.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }

        [DisplayName("Nome do Produto")]
        public string NomeProduto { get; set; }

        [DisplayName("Preço do Produto")]
        public decimal PrecoProduto { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        public string? Tipo { get; set; }
        public bool Desconto { get; set; }
        public int Tamanho { get; set; }

        /*[Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [DisplayName("Quantidade em estoque")]*/

        [DisplayName("Quantidade")]
        public int QuantidadeProd { get; set; }

        //[Required(ErrorMessage = "A imagem é obrigatória")]
        [DisplayName("Imagem do produto")]
        public string? ImagemProduto { get; set; }

        [DisplayName("Marca")]
        public int Fk_IdMarca { get; set; }

        [DisplayName("Marca")]
        public MarcaProduto MarcaProdutos { get; set; }
        public ICollection<ClienteFav> ClienteFavs { get; set; }
        public ICollection<ItemPedido> ItemPedidos { get; set; }
        /*public ICollection<ItemCarrinho> ItemCarrinhos { get; set; }*/
    }
}
