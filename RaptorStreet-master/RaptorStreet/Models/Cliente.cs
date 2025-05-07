using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.Xml;

namespace RaptorStreet.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateOnly DataNascimento { get; set; }
        public int CPF { get; set; }
        public int Telefone { get; set; }
        public string SenhaCliente { get; set; }
        public string EmailCliente { get; set; }

        public ICollection<Login> Logins { get; set; }
        public ICollection<ClienteEndereco> ClienteEnderecos { get; set; }
        public ICollection<ClienteFav> ClienteFavs { get; set; }
        public ICollection<Carrinho> Carrinhos { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

    }
}
