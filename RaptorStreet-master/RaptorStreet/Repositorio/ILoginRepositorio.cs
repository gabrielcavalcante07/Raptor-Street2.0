using RaptorStreet.Models;

namespace RaptorStreet.Repositorio.Interface
{
    public interface ILoginRepositorio
    {
        Cliente Login(string EmailCliente, string SenhaCliente);
        Adm LoginAdm(string EmailAdm, string SenhaAdm);
    }
}
