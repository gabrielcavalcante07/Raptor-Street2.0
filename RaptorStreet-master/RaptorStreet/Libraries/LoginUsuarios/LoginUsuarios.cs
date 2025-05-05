using MySqlX.XDevAPI;
using RaptorStreet.Models;
using Newtonsoft.Json;

namespace RaptorStreet.Libraries.LoginUsuarios
{ 

    public class LoginUsuarios
    {
        //injeção de depencia
        private string Key = "Login.Usuario";
        private Sessao.Sessao _sessao;

        //Construtor
        public LoginUsuarios(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente login)
        {
            // Serializar- Com a serialização é possível salvar objetos em arquivos de dados
            string loginJSONString = JsonConvert.SerializeObject(login);
        }

        public LoginUsuarios GetCliente()
        {
            /* Deserializar-Já a desserialização permite que os 
            objetos persistidos em arquivos possam ser recuperados e seus valores recriados na memória*/

            if (_sessao.Existe(Key))
            {
                string loginJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<LoginUsuarios>(loginJSONString);
            }
            else
            {
                return null;
            }
        }
        //Remove a sessão e desloga o Cliente
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}


