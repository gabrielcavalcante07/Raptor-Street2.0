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
    public class ItemPedidoesController : Controller
    {
        private readonly RaptorDBContext _context;

        public ItemPedidoesController(RaptorDBContext context)
        {
            _context = context;
        }

        // GET: ItemPedidoes
        public async Task<IActionResult> Index()
        {
            var raptorDBContext = _context.ItemPedidos.Include(i => i.Pedidos).Include(i => i.Produtos);
            return View(await raptorDBContext.ToListAsync());
        }

        // GET: ItemPedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedidos)
                .Include(i => i.Produtos)
                .FirstOrDefaultAsync(m => m.IdProdutoPedido == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // GET: ItemPedidoes/Create
        public IActionResult Create()
        {
            ViewData["Fk_IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            ViewData["Fk_IdProduto"] = new SelectList(_context.Produtos, "IdProduto", "IdProduto");
            return View();
        }

        // POST: ItemPedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProdutoPedido,Fk_IdPedido,Fk_IdProduto,PrecoUnitario")] ItemPedido itemPedido)
        {
           
                _context.Add(itemPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["Fk_IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", itemPedido.Fk_IdPedido);
            ViewData["Fk_IdProduto"] = new SelectList(_context.Produtos, "IdProduto", "IdProduto", itemPedido.Fk_IdProduto);
            return View(itemPedido);
        }

        // GET: ItemPedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos.FindAsync(id);
            if (itemPedido == null)
            {
                return NotFound();
            }
            ViewData["Fk_IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", itemPedido.Fk_IdPedido);
            ViewData["Fk_IdProduto"] = new SelectList(_context.Produtos, "IdProduto", "IdProduto", itemPedido.Fk_IdProduto);
            return View(itemPedido);
        }

        // POST: ItemPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProdutoPedido,Fk_IdPedido,Fk_IdProduto,PrecoUnitario")] ItemPedido itemPedido)
        {
            if (id != itemPedido.IdProdutoPedido)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(itemPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemPedidoExists(itemPedido.IdProdutoPedido))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["Fk_IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", itemPedido.Fk_IdPedido);
            ViewData["Fk_IdProduto"] = new SelectList(_context.Produtos, "IdProduto", "IdProduto", itemPedido.Fk_IdProduto);
            return View(itemPedido);
        }

        // GET: ItemPedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedidos)
                .Include(i => i.Produtos)
                .FirstOrDefaultAsync(m => m.IdProdutoPedido == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // POST: ItemPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemPedido = await _context.ItemPedidos.FindAsync(id);
            if (itemPedido != null)
            {
                _context.ItemPedidos.Remove(itemPedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemPedidoExists(int id)
        {
            return _context.ItemPedidos.Any(e => e.IdProdutoPedido == id);
        }
    }
}
