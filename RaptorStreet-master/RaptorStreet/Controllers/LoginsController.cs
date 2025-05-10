using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaptorStreet.Data;
using RaptorStreet.Models;
using RaptorStreet.Repositorio.Interface;

namespace RaptorStreet.Controllers
{
    public class LoginsController : Controller
    {
        private readonly RaptorDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoginRepositorio _loginRepositorio;

        public LoginsController(RaptorDBContext context, IHttpContextAccessor httpcontextacessor, ILoginRepositorio loginRepositorio)
        {
            _context = context;
            _httpContextAccessor = httpcontextacessor;
            _loginRepositorio = loginRepositorio;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            var RaptorDBContext = _context.Logins.Include(l => l.Adms).Include(l => l.Clientes);
            return View(await RaptorDBContext.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.Adms)
                .Include(l => l.Clientes)
                .FirstOrDefaultAsync(m => m.IdLogin == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            ViewData["IdAdm"] = new SelectList(_context.Adms, "IdAdm", "IdAdm");
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLogin,IdCliente,IdAdm")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdm"] = new SelectList(_context.Adms, "IdAdm", "IdAdm", login.IdAdm);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", login.IdCliente);
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["IdAdm"] = new SelectList(_context.Adms, "IdAdm", "IdAdm", login.IdAdm);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", login.IdCliente);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLogin,IdCliente,IdAdm")] Login login)
        {
            if (id != login.IdLogin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.IdLogin))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdm"] = new SelectList(_context.Adms, "IdAdm", "IdAdm", login.IdAdm);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", login.IdCliente);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.Adms)
                .Include(l => l.Clientes)
                .FirstOrDefaultAsync(m => m.IdLogin == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login = await _context.Logins.FindAsync(id);
            if (login != null)
            {
                _context.Logins.Remove(login);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(int id)
        {
            return _context.Logins.Any(e => e.IdLogin == id);
        }

        // ---------------------------- LOGIN -------------------------------
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            // Verifica se já existe um usuário logado
            if (HttpContext.Session.GetString("EmailCliente") != null ||
                HttpContext.Session.GetString("EmailAdm") != null)
            {
                TempData["Login"] = "Faça o logout antes de trocar de conta.";
                return RedirectToAction("Index", "Home");
            }

            var login = _loginRepositorio.Login(email, senha);

            if (login is Cliente Clientes)
            {
                HttpContext.Session.SetString("EmailCliente", email);
                HttpContext.Session.SetInt32("IdCliente", Clientes.IdCliente);
                TempData["Login"] = "Bem-vindo, cliente!";
                return RedirectToAction("Index", "Home");
            }
            else if (login is Adm)
            {
                HttpContext.Session.SetString("EmailAdm", email);
                TempData["Login"] = "Bem-vindo, Administrador!";
                return RedirectToAction("Index", "Adms");
            }

            TempData["Login"] = "E-mail ou senha inválidos.";
            return RedirectToAction("Login", "Logins");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Login"] = "Você foi desconectado.";
            return RedirectToAction("Index", "Home");
        }


    }
}

