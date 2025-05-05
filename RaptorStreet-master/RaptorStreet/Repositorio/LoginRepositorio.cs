using RaptorStreet.Data;
using RaptorStreet.Libraries.LoginUsuarios;
using RaptorStreet.Models;
using RaptorStreet.Repositorio.Interface;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;

namespace RaptorStreet.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly RaptorDBContext _context;
        private readonly string? _conexaoMySQL;

        public LoginRepositorio(RaptorDBContext context, IConfiguration conf)
        {
            _context = context;
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
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

        //Login Cliente(metodo)
        public Cliente LoginSql(string EmailCliente, string SenhaCliente)
        {
            //usando a variavel conexao 
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                //abre a conexão com o banco de dados
                conexao.Open();

                // variavel cmd que receb o select do banco de dados buscando email e senha
                MySqlCommand cmd = new MySqlCommand("select * from tbCliente where EmailCliente = @Usuario and SenhaCliente = @Senha", conexao);

                //os paramentros do usuario e da senha 
                cmd.Parameters.Add("@Usuario", MySqlDbType.VarChar).Value = EmailCliente;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = SenhaCliente;

                //Le os dados que foi pego do email e senha do banco de dados
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //guarda os dados que foi pego do email e senha do banco de dados
                MySqlDataReader dr;

                //instanciando a model Login
                Cliente login = new Cliente();
                //executando os comandos do mysql e passsando paa a variavel dr
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //verifica todos os dados que foram pego do banco e pega o email e senha
                while (dr.Read())
                {

                    login.EmailCliente = Convert.ToString(dr["usuario"]);
                    login.SenhaCliente = Convert.ToString(dr["senha"]);
                }
                return _context.Clientes
                .FirstOrDefault(c => c.EmailCliente == EmailCliente && c.SenhaCliente == SenhaCliente);
            }
        }
    }

}
