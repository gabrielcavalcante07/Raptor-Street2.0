using RaptorStreet.Models;

namespace RaptorStreet.Repositorio.Interface
{
    public interface ILoginRepositorio
    {
        object Login(string email, string senha);

        //Cadastrar Cliente
        void Cadastrar(Cliente cliente);

        //Buscar Todos os clientes
        IEnumerable<Cliente> TodosClientes();

        //Busca todos por id
        Cliente ObterCliente(int id);


        //Atualizar Cliente
        void Atualizar(Cliente cliente);

        //Excluir
        void Excluir(int id);
    }
}
