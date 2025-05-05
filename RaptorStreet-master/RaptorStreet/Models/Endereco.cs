using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.ConstrainedExecution;

namespace RaptorStreet.Models
{
    public class Endereco
    {
        public int IdEndereco { get; set; }
        public string CEP { get; set; }
        public int NumeroEndereco { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public ICollection<ClienteEndereco> ClienteEnderecos { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

    }
}
