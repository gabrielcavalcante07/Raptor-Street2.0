using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaptorStreet.Data;
using RaptorStreet.Models;

namespace RaptorStreet.Controllers
{
    public class ClienteFavsController : Controller
    {
        private readonly RaptorDBContext _context;

        public ClienteFavsController(RaptorDBContext context)
        {
            _context = context;
        }

        // GET: ClienteFavs
        public async Task<IActionResult> Index()
        {
            var raptorDBContext = _context.ClienteFavs.Include(c => c.Clientes).Include(c => c.Produtos);
            return View(await raptorDBContext.ToListAsync());
        }

        // GET: ClienteFavs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFav = await _context.ClienteFavs
                .Include(c => c.Clientes)
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(m => m.IdClienteFav == id);
            if (clienteFav == null)
            {
                return NotFound();
            }

            return View(clienteFav);
        }

        // GET: ClienteFavs/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdProduto"] = new SelectList(_context.Produtos, "IdProduto", "IdProduto");
            return View();
        }

        // POST: ClienteFavs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClienteFav,IdCliente,IdProduto,ativado")] ClienteFav clienteFav)
        {
         
                _context.Add(clienteFav);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteFav.IdCliente);
            ViewData["IdProduto"] = new SelectList(_context.Produtos, "IdProduto", "IdProduto", clienteFav.IdProduto);
            return View(clienteFav);
        }

        // GET: ClienteFavs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFav = await _context.ClienteFavs.FindAsync(id);
            if (clienteFav == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteFav.IdCliente);
            ViewData["IdProduto"] = new SelectList(_context.Produtos, "IdProduto", "IdProduto", clienteFav.IdProduto);
            return View(clienteFav);
        }

        // POST: ClienteFavs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClienteFav,IdCliente,IdProduto,ativado")] ClienteFav clienteFav)
        {
            if (id != clienteFav.IdClienteFav)
            {
                return NotFound();
            }

        
                try
                {
                    _context.Update(clienteFav);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteFavExists(clienteFav.IdClienteFav))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteFav.IdCliente);
            ViewData["IdProduto"] = new SelectList(_context.Produtos, "IdProduto", "IdProduto", clienteFav.IdProduto);
            return View(clienteFav);
        }

        // GET: ClienteFavs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFav = await _context.ClienteFavs
                .Include(c => c.Clientes)
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(m => m.IdClienteFav == id);
            if (clienteFav == null)
            {
                return NotFound();
            }

            return View(clienteFav);
        }

        // POST: ClienteFavs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteFav = await _context.ClienteFavs.FindAsync(id);
            if (clienteFav != null)
            {
                _context.ClienteFavs.Remove(clienteFav);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteFavExists(int id)
        {
            return _context.ClienteFavs.Any(e => e.IdClienteFav == id);
        }
    }
}
