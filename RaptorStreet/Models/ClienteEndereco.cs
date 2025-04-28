using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RaptorStreet.Models
{
    public class ClienteEndereco
    {
        public int IdEndCliente { get; set; }
        public int IdEnd { get; set; }
        public Endereco Enderecos { get; set; }
        public int Fk_IdCliente { get; set; }
        public Cliente Clientes { get; set; }


    }
}

