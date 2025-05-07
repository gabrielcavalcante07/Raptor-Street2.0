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
        private readonly string _conexaoMySQL;
        public LoginRepositorio(RaptorDBContext context, IConfiguration configuration)
        {
            _context = context;
            _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");
        }
        public object Login(string email, string senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // parte de cleinte
                var cmdCliente = new MySqlCommand("SELECT * FROM tbCliente WHERE emailCliente = @Email AND senhaCliente = @Senha", conexao);
                cmdCliente.Parameters.AddWithValue("@Email", email);
                cmdCliente.Parameters.AddWithValue("@Senha", senha);

                using (var dr = cmdCliente.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var cliente = new Cliente
                        {
                            IdCliente = Convert.ToInt32(dr["idCliente"]),
                            NomeCliente = dr["nomeCompleto"].ToString(),
                            EmailCliente = dr["emailCliente"].ToString(),
                            SenhaCliente = dr["senhaCliente"].ToString()
                        };
                        return cliente;
                    }
                }

                // parte de adm
                var cmdAdm = new MySqlCommand("SELECT * FROM tbAdm WHERE emailAdm = @Email AND senhaAdm = @Senha", conexao);
                cmdAdm.Parameters.AddWithValue("@Email", email);
                cmdAdm.Parameters.AddWithValue("@Senha", senha);

                using (var dr = cmdAdm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var adm = new Adm
                        {
                            IdAdm = Convert.ToInt32(dr["idAdm"]),
                            NomeAdm = dr["nomeAdm"].ToString(),
                            EmailAdm = dr["emailAdm"].ToString(),
                            SenhaAdm = dr["senhaAdm"].ToString()
                        };
                        return adm;
                    }
                }

                return null;
            }
        }

    }
}
