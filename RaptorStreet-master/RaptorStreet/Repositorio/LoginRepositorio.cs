using RaptorStreet.Data;
using RaptorStreet.Models;
using RaptorStreet.Repositorio.Interface;
using System.Linq;

namespace RaptorStreet.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {

        private readonly RaptorDBContext _context;

        public LoginRepositorio(RaptorDBContext context)
        {
            _context = context;
        }

        public Cliente Login(string EmailCliente, string SenhaCliente)
        {
            return _context.Clientes
                .FirstOrDefault(c => c.EmailCliente == EmailCliente && c.SenhaCliente == SenhaCliente);
        }

        public Adm LoginAdm(string EmailAdm, string SenhaAdm)
        {
            return _context.Adms
                .FirstOrDefault(a => a.EmailAdm == EmailAdm && a.SenhaAdm == SenhaAdm);
        }
    }
}
