namespace RaptorStreet.Models
{
    public class Login
    {
        public int IdLogin { get; set; }
        public int IdCliente { get; set; }
        public int IdAdm { get; set; }

        public Cliente Clientes { get; set; }
        public Adm Adms { get; set; }

    }
}
