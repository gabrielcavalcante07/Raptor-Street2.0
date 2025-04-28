using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
namespace RaptorStreet.Models
{
    public class MarcaProduto
    {
        public int IdMarca { get; set; }
        public string NomeMarca { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
