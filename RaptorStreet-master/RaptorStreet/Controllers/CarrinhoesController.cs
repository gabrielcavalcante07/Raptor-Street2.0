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
    public class CarrinhoesController : Controller
    {
        private readonly RaptorDBContext _context;

        public CarrinhoesController(RaptorDBContext context)
        {
            _context = context;
        }

        // GET: Carrinhoes
        public async Task<IActionResult> Index()
        {
            var raptorDBContext = _context.Carrinhos.Include(c => c.Clientes);
            return View(await raptorDBContext.ToListAsync());
        }

        // GET: Carrinhoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinhos
                .Include(c => c.Clientes)
                .FirstOrDefaultAsync(m => m.IdCarrinho == id);
            if (carrinho == null)
            {
                return NotFound();
            }

            return View(carrinho);
        }

        // GET: Carrinhoes/Create
        public IActionResult Create()
        {
            ViewData["Fk_IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            return View();
        }

        // POST: Carrinhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarrinho,TipoProduto,TotalVenda,Qtd,DataVenda,Fk_IdCliente")] Carrinho carrinho)
        {

                _context.Add(carrinho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["Fk_IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", carrinho.Fk_IdCliente);
            return View(carrinho);
        }

        // GET: Carrinhoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinhos.FindAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }
            ViewData["Fk_IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", carrinho.Fk_IdCliente);
            return View(carrinho);
        }

        // POST: Carrinhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarrinho,TipoProduto,TotalVenda,Qtd,DataVenda,Fk_IdCliente")] Carrinho carrinho)
        {
            if (id != carrinho.IdCarrinho)
            {
                return NotFound();
            }

     
                try
                {
                    _context.Update(carrinho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrinhoExists(carrinho.IdCarrinho))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["Fk_IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", carrinho.Fk_IdCliente);
            return View(carrinho);
        }

        // GET: Carrinhoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinhos
                .Include(c => c.Clientes)
                .FirstOrDefaultAsync(m => m.IdCarrinho == id);
            if (carrinho == null)
            {
                return NotFound();
            }

            return View(carrinho);
        }

        // POST: Carrinhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);
            if (carrinho != null)
            {
                _context.Carrinhos.Remove(carrinho);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrinhoExists(int id)
        {
            return _context.Carrinhos.Any(e => e.IdCarrinho == id);
        }
    }
}
