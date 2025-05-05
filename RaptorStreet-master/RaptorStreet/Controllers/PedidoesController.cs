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
    public class PedidoesController : Controller
    {
        private readonly RaptorDBContext _context;

        public PedidoesController(RaptorDBContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var raptorDBContext = _context.Pedidos.Include(p => p.Clientes).Include(p => p.Enderecos).Include(p => p.NotaFiscals).Include(p => p.Pagamentos);
            return View(await raptorDBContext.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Clientes)
                .Include(p => p.Enderecos)
                .Include(p => p.NotaFiscals)
                .Include(p => p.Pagamentos)
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["Fk_IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["Fk_IdEndereco"] = new SelectList(_context.Enderecos, "IdEndereco", "IdEndereco");
            ViewData["Fk_IdNota"] = new SelectList(_context.NotaFiscals, "IdNota", "IdNota");
            ViewData["Fk_IdPag"] = new SelectList(_context.Pagamentos, "IdPag", "IdPag");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,Fk_IdNota,Fk_IdEndereco,Fk_IdPag,Fk_IdCliente,dataPed,totalPedido")] Pedido pedido)
        {
            
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["Fk_IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", pedido.Fk_IdCliente);
            ViewData["Fk_IdEndereco"] = new SelectList(_context.Enderecos, "IdEndereco", "IdEndereco", pedido.Fk_IdEndereco);
            ViewData["Fk_IdNota"] = new SelectList(_context.NotaFiscals, "IdNota", "IdNota", pedido.Fk_IdNota);
            ViewData["Fk_IdPag"] = new SelectList(_context.Pagamentos, "IdPag", "IdPag", pedido.Fk_IdPag);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["Fk_IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", pedido.Fk_IdCliente);
            ViewData["Fk_IdEndereco"] = new SelectList(_context.Enderecos, "IdEndereco", "IdEndereco", pedido.Fk_IdEndereco);
            ViewData["Fk_IdNota"] = new SelectList(_context.NotaFiscals, "IdNota", "IdNota", pedido.Fk_IdNota);
            ViewData["Fk_IdPag"] = new SelectList(_context.Pagamentos, "IdPag", "IdPag", pedido.Fk_IdPag);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,Fk_IdNota,Fk_IdEndereco,Fk_IdPag,Fk_IdCliente,dataPed,totalPedido")] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.IdPedido))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["Fk_IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", pedido.Fk_IdCliente);
            ViewData["Fk_IdEndereco"] = new SelectList(_context.Enderecos, "IdEndereco", "IdEndereco", pedido.Fk_IdEndereco);
            ViewData["Fk_IdNota"] = new SelectList(_context.NotaFiscals, "IdNota", "IdNota", pedido.Fk_IdNota);
            ViewData["Fk_IdPag"] = new SelectList(_context.Pagamentos, "IdPag", "IdPag", pedido.Fk_IdPag);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Clientes)
                .Include(p => p.Enderecos)
                .Include(p => p.NotaFiscals)
                .Include(p => p.Pagamentos)
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedido == id);
        }
    }
}
