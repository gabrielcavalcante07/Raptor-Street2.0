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
            // COLOCAR ISSO NA CONTYROLLER DO ADMMM  cliente.datacad_User = DateTime.Now;
            //Cliente cliente = await _context.Clientes.FirstOrDefault(c => c.idUser == usuario.idUser);
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            // Criando a lista de claims
            //Claims são um tipo de identificadores do usuario
            var claims = new List<Claim> // guarda os dados dos usuarios
            {
                new Claim(ClaimTypes.Name, cliente.EmailCliente),
                new Claim(ClaimTypes.SerialNumber, Convert.ToString(cliente.IdCliente)),
                // Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber)?.Value)
                new Claim(ClaimTypes.Role, "Cliente")
            };
            //[Authorize(Roles = "Usuario")] para tipos especificos
            //[Authorize] logado

            //Criando o Claim de identidade do usuario, juntamente de coockies
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //Permite que o usuario continue logado mesmo se fechar o navegador
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Mantém o cookie ao fechar o navegador
            };
            //Vai logar o usuario com o HTTP usando tanto os coockies quanto a identidade do usuario
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            TempData["Login"] = "Cadastro efetuado com sucesso!";
            return RedirectToAction("Index", "Home");



        }
    }
}
