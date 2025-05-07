using RaptorStreet.Models;

namespace RaptorStreet.Repositorio.Interface
{
    public interface ILoginRepositorio
    {
        object Login(string email, string senha);
    }
}
