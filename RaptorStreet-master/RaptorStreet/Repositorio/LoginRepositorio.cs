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
using K4os.Compression.LZ4.Internal;

namespace RaptorStreet.Repositorio
{

    public class LoginRepositorio : ILoginRepositorio
    {
        //faz as conexões com mysql e dbcontext 
        private readonly RaptorDBContext _context;
        private readonly string _conexaoMySQL;
        public LoginRepositorio(RaptorDBContext context, IConfiguration configuration)
        {
            _context = context;
            _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");
        }

        //passa os paremetros na mesma ordem
        public object Login(string email, string senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // nucleo cliente
                /*passa parâmetros onde se o senha cliente = senhaCliente = cria um obj do tipo cliente no login passando os
                parâmetros relacionados ao banco de dados que retorna o cliente*/
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
        //Cadastrar Cliente
        public void Cadastrar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))

            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into cliente (nome,telefone,email) values (@nome, @telefone, @email)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.NomeCliente;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.EmailCliente;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }

        public IEnumerable<Cliente> TodosClientes()
        {
            throw new NotImplementedException();
        }

        public Cliente ObterCliente(int id)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }
    }
}
