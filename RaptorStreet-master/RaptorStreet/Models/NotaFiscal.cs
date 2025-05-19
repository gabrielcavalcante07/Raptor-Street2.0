using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RaptorStreet.Models
{
    public class NotaFiscal
    {
        public int IdNota { get; set; }
        public DateOnly dataNF { get; set; }
        public decimal valorNF { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}
