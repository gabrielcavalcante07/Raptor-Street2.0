using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.Xml;
using System.ComponentModel;

namespace RaptorStreet.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        [DisplayName("Cliente")]
        public string NomeCliente { get; set; }

        [DisplayName("Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [DisplayName("CPF")]
        public string CPF { get; set; }

        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [DisplayName("Senha")]
        public string? SenhaCliente { get; set; }

        [DisplayName("Email")]
        public string? EmailCliente { get; set; }

        public ICollection<Login> Logins { get; set; }
        public ICollection<ClienteEndereco> ClienteEnderecos { get; set; }
        public ICollection<ClienteFav> ClienteFavs { get; set; }
        /*public ICollection<Carrinho> Carrinhos { get; set; }*/
        public ICollection<Pedido> Pedidos { get; set; }

    }
}
