using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace RaptorStreet.Models
{
    public class Carrinho
    {
        public int IdCarrinho { get; set; }
        public int Fk_IdCliente { get; set; }
        public Cliente Clientes { get; set; }

        public ICollection<ItemCarrinho> ItemCarrinhos { get; set; }
    }


}
