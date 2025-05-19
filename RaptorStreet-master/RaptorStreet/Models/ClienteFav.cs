using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Xml;

namespace RaptorStreet.Models
{
    public class ClienteFav
    {
        public int IdClienteFav { get; set;}
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public Boolean ativado { get; set; }

        public Cliente Clientes { get; set; }
        public Produto Produtos { get; set; }
    }
}
