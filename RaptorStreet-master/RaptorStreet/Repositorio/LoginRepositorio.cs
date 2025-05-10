using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MySqlX.XDevAPI;
using RaptorStreet.Data;
using RaptorStreet.Models;
using RaptorStreet.Repositorio.Interface;
using System.Configuration;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

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

                // nucleo cliente
                var cmdCliente = new MySqlCommand("SELECT * FROM tbClientes WHERE EmailCliente = @Email AND SenhaCliente = @Senha", conexao);
                cmdCliente.Parameters.AddWithValue("@Email", email);
                cmdCliente.Parameters.AddWithValue("@Senha", senha);

                using (var dr = cmdCliente.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var cliente = new Cliente
                        {
                            IdCliente = Convert.ToInt32(dr["IdCliente"]),
                            NomeCliente = dr["NomeCliente"].ToString(),
                            EmailCliente = dr["EmailCliente"].ToString(),
                            SenhaCliente = dr["SenhaCliente"].ToString()
                        };
                        return cliente;
                    }
                }

                // nucleo adm
                var cmdAdm = new MySqlCommand("SELECT * FROM tbAdm WHERE emailAdm = @Email AND senhaAdm = @Senha", conexao);
                cmdAdm.Parameters.AddWithValue("@Email", email);
                cmdAdm.Parameters.AddWithValue("@Senha", senha);

                using (var dr = cmdAdm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var adm = new Adm
                        {
                            IdAdm = Convert.ToInt32(dr["IdAdm"]),
                            NomeAdm = dr["NomeAdm"].ToString(),
                            EmailAdm = dr["EmailAdm"].ToString(),
                            SenhaAdm = dr["SenhaAdm"].ToString()
                        };
                        return adm;
                    }
                }

                return null;
            }
        }

    }
}
