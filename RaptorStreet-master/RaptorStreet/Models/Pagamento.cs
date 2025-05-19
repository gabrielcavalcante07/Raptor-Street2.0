using Microsoft.AspNetCore.Http.HttpResults;

namespace RaptorStreet.Models
{
    public class Pagamento
    {
        public int IdPag { get; set; }
        public string StatusPag { get; set; }
        public string MetodoPag { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}
