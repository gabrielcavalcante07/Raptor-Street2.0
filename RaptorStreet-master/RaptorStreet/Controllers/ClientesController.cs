using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaptorStreet.Data;
using RaptorStreet.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RaptorStreet.Repositorio;
using RaptorStreet.Repositorio.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RaptorStreet.Controllers
{
    public class ClientesController : Controller
    {
        private readonly RaptorDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientesController(RaptorDBContext context, IHttpContextAccessor httpcontextacessor, ILoginRepositorio loginRepositorio)
        {
            _context = context;
            _httpContextAccessor = httpcontextacessor;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,NomeCliente,DataNascimento,CPF,Telefone,SenhaCliente,EmailCliente")] Cliente cliente)
        {

                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,NomeCliente,DataNascimento,CPF,Telefone,SenhaCliente,EmailCliente")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

       
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }

        public async Task<IActionResult> Cadastro(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.EmailCliente))
            {
                ModelState.AddModelError("EmailCliente", "O email é obrigatório.");
                return View(cliente);  // Retorna a view com erro de validação
            }

            if (_context.Clientes.Any(c => c.EmailCliente == cliente.EmailCliente))
            {
                ModelState.AddModelError("EmailCliente", "Este email já está registrado.");
                return View(cliente);  // Retorna a view com erro de validação
            }

            try
            {
                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return View(cliente);  // Retorna a view com erro
            }

            // Armazenar o ID do cliente na sessão 
            HttpContext.Session.SetInt32("IdCliente", cliente.IdCliente);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cliente.EmailCliente),
                new Claim(ClaimTypes.SerialNumber, cliente.IdCliente.ToString()),
                new Claim(ClaimTypes.Role, "Cliente")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            TempData["Login"] = "Cadastro efetuado com sucesso!!!";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Painel()
        {
            var idCliente = HttpContext.Session.GetInt32("IdCliente");

            if (idCliente == null)
            {
                TempData["Login"] = "É necessário estar logado para acessar o painel.";
                return RedirectToAction("Login", "Logins");
            }

            var cliente = await _context.Clientes
                .Include(c => c.ClienteEnderecos)
                    .ThenInclude(ce => ce.Enderecos)
                .FirstOrDefaultAsync(c => c.IdCliente == idCliente);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> EditarCliente(int IdCliente, string Nome, string Email, int Telefone)
        {
            var cliente = await _context.Clientes.FindAsync(IdCliente);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.NomeCliente = Nome;
            cliente.EmailCliente = Email;
            cliente.Telefone = Telefone;

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();

            TempData["Msg"] = "Seus Dados foram atualizados com sucesso!";
            return RedirectToAction("Painel");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEndereco(int idCliente, string CEP, int NumeroEndereco, string Logradouro, string Complemento, string Bairro, string Cidade, string Estado)
        {
            // Cria o novo endereço
            var novoEndereco = new Endereco
            {
                CEP = CEP,
                NumeroEndereco = NumeroEndereco,
                Logradouro = Logradouro,
                Complemento = Complemento,
                Bairro = Bairro,
                Cidade = Cidade,
                Estado = Estado
            };

            // Adiciona o novo endereço ao banco de dados
            _context.Enderecos.Add(novoEndereco);
            await _context.SaveChangesAsync();

            // Cria a ligação entre Cliente e Endereço (tabela ClienteEndereco)
            var clienteEndereco = new ClienteEndereco
            {
                IdEnd = novoEndereco.IdEndereco, // Chave primária do Endereço
                Fk_IdCliente = idCliente // ID do cliente logado
            };

            // Adiciona a relação Cliente-Endereco
            _context.ClienteEnderecos.Add(clienteEndereco);
            await _context.SaveChangesAsync();

            TempData["Msg"] = "Endereço adicionado com sucesso!";
            return RedirectToAction("Painel");
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEndereco(int idEndereco, int idCliente)
        {
            // Remove a relação ClienteEndereco primeiro
            var clienteEndereco = await _context.ClienteEnderecos
                .FirstOrDefaultAsync(ce => ce.IdEnd == idEndereco && ce.Fk_IdCliente == idCliente);

            if (clienteEndereco != null)
            {
                _context.ClienteEnderecos.Remove(clienteEndereco);
            }

            // Agora remove o endereço
            var endereco = await _context.Enderecos.FindAsync(idEndereco);
            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
            }

            await _context.SaveChangesAsync();

            TempData["Msg"] = "Endereço removido com sucesso!";
            return RedirectToAction("Painel");
        }
    }
}
