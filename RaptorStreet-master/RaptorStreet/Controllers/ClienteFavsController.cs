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
    public class ClienteFavoritoesController : Controller
    {
        private readonly RaptorDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClienteFavoritoesController(RaptorDBContext context, IHttpContextAccessor httpcontextacessor, ILoginRepositorio loginRepositorio)
        {
            _context = context;
            _httpContextAccessor = httpcontextacessor;
        }

        // GET: ClienteFavoritoes
        public async Task<IActionResult> Index()
        {
            var clienteFavoritos = _context.ClienteFavs.Include(cl => cl.Produtos).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;

            var idCliente = HttpContext.Session.GetInt32("IdCliente");
            if (idCliente == null)
            {
                return RedirectToAction("Logins", "Login");
            }

            var favoritos = await _context.ClienteFavs
                .Include(cl => cl.Produtos)
                .Where(cl => cl.IdCliente == idCliente && cl.ativado)
                .ToListAsync();

            return View(favoritos);
        }



        // GET: ClienteFavoritoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var clienteFavoritos = _context.ClienteFavs.Include(cl => cl.Produtos).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavs
                .Include(c => c.Clientes)
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(m => m.IdClienteFav == id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }

            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Create
        public IActionResult Create()
        {
            var clienteFavoritos = _context.ClienteFavs.Include(cl => cl.Produtos).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente");
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd");
            return View();
        }

        // POST: ClienteFavoritoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClienteFav,IdCliente,IdProd,Ativo")] ClienteFav clienteFavorito)
        {
            var clienteFavoritos = _context.ClienteFavs.Include(cl => cl.Produtos).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            if (ModelState.IsValid)
            {
                _context.Add(clienteFavorito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.IdCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.IdProduto);
            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var clienteFavoritos = _context.ClienteFavs.Include(cl => cl.Produtos).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavs.FindAsync(id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.IdCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.IdProduto);
            return View(clienteFavorito);
        }

        // POST: ClienteFavoritoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClienteFav,IdCliente,IdProd,Ativo")] ClienteFav clienteFavorito)
        {
            var clienteFavoritos = _context.ClienteFavs.Include(cl => cl.Produtos).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            if (id != clienteFavorito.IdClienteFav)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteFavorito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteFavoritoExists(clienteFavorito.IdClienteFav))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.IdCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.IdProduto);
            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var clienteFavoritos = _context.ClienteFavs.Include(cl => cl.Produtos).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavs
                .Include(c => c.Clientes)
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(m => m.IdClienteFav == id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }

            return View(clienteFavorito);
        }

        // POST: ClienteFavoritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteFavoritos = _context.ClienteFavs.Include(cl => cl.Produtos).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            var clienteFavorito = await _context.ClienteFavs.FindAsync(id);
            if (clienteFavorito != null)
            {
                _context.ClienteFavs.Remove(clienteFavorito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteFavoritoExists(int id)
        {
            return _context.ClienteFavs.Any(e => e.IdClienteFav == id);
        }


        //FAVORITOS

        [HttpPost]
        public IActionResult ToggleFavorito([FromBody] int idProduto)
        {
            var idCliente = HttpContext.Session.GetInt32("idCliente");

            if (idCliente == null)
            {
                return Json(new { sucesso = false, mensagem = "Usuário não autenticado." });
            }

            var favoritoExistente = _context.ClienteFavs
                .FirstOrDefault(f => f.IdCliente == idCliente && f.IdProduto == idProduto);

            if (favoritoExistente != null)
            {
                _context.ClienteFavs.Remove(favoritoExistente);
            }
            else
            {
                var novoFav = new ClienteFav
                {
                    IdCliente = idCliente.Value,
                    IdProduto = idProduto,
                    ativado = true
                };
                _context.ClienteFavs.Add(novoFav);
            }

            _context.SaveChanges();

            return Json(new { sucesso = true });
        }


}

}
