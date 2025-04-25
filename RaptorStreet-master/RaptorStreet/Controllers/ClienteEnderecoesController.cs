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
    public class ClienteEnderecoesController : Controller
    {
        private readonly RaptorDBContext _context;

        public ClienteEnderecoesController(RaptorDBContext context)
        {
            _context = context;
        }

        // GET: ClienteEnderecoes
        public async Task<IActionResult> Index()
        {
            var raptorDBContext = _context.ClienteEnderecos.Include(c => c.Clientes).Include(c => c.Enderecos);
            return View(await raptorDBContext.ToListAsync());
        }

        // GET: ClienteEnderecoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteEndereco = await _context.ClienteEnderecos
                .Include(c => c.Clientes)
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(m => m.IdEndCliente == id);
            if (clienteEndereco == null)
            {
                return NotFound();
            }

            return View(clienteEndereco);
        }

        // GET: ClienteEnderecoes/Create
        public IActionResult Create()
        {
            ViewData["IdEndCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdEndCliente"] = new SelectList(_context.Enderecos, "IdEndereco", "IdEndereco");
            return View();
        }

        // POST: ClienteEnderecoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEndCliente,IdEnd,Fk_IdCliente")] ClienteEndereco clienteEndereco)
        {

                _context.Add(clienteEndereco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["IdEndCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteEndereco.IdEndCliente);
            ViewData["IdEndCliente"] = new SelectList(_context.Enderecos, "IdEndereco", "IdEndereco", clienteEndereco.IdEndCliente);
            return View(clienteEndereco);
        }

        // GET: ClienteEnderecoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteEndereco = await _context.ClienteEnderecos.FindAsync(id);
            if (clienteEndereco == null)
            {
                return NotFound();
            }
            ViewData["IdEndCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteEndereco.IdEndCliente);
            ViewData["IdEndCliente"] = new SelectList(_context.Enderecos, "IdEndereco", "IdEndereco", clienteEndereco.IdEndCliente);
            return View(clienteEndereco);
        }

        // POST: ClienteEnderecoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEndCliente,IdEnd,Fk_IdCliente")] ClienteEndereco clienteEndereco)
        {
            if (id != clienteEndereco.IdEndCliente)
            {
                return NotFound();
            }

    
                try
                {
                    _context.Update(clienteEndereco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteEnderecoExists(clienteEndereco.IdEndCliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["IdEndCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteEndereco.IdEndCliente);
            ViewData["IdEndCliente"] = new SelectList(_context.Enderecos, "IdEndereco", "IdEndereco", clienteEndereco.IdEndCliente);
            return View(clienteEndereco);
        }

        // GET: ClienteEnderecoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteEndereco = await _context.ClienteEnderecos
                .Include(c => c.Clientes)
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(m => m.IdEndCliente == id);
            if (clienteEndereco == null)
            {
                return NotFound();
            }

            return View(clienteEndereco);
        }

        // POST: ClienteEnderecoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteEndereco = await _context.ClienteEnderecos.FindAsync(id);
            if (clienteEndereco != null)
            {
                _context.ClienteEnderecos.Remove(clienteEndereco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteEnderecoExists(int id)
        {
            return _context.ClienteEnderecos.Any(e => e.IdEndCliente == id);
        }
    }
}
