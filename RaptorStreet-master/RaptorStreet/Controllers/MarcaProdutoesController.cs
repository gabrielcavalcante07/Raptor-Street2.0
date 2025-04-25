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
    public class MarcaProdutoesController : Controller
    {
        private readonly RaptorDBContext _context;

        public MarcaProdutoesController(RaptorDBContext context)
        {
            _context = context;
        }

        // GET: MarcaProdutoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MarcaProdutos.ToListAsync());
        }

        // GET: MarcaProdutoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaProduto = await _context.MarcaProdutos
                .FirstOrDefaultAsync(m => m.IdMarca == id);
            if (marcaProduto == null)
            {
                return NotFound();
            }

            return View(marcaProduto);
        }

        // GET: MarcaProdutoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MarcaProdutoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMarca,NomeMarca")] MarcaProduto marcaProduto)
        {
            
                _context.Add(marcaProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(marcaProduto);
        }

        // GET: MarcaProdutoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaProduto = await _context.MarcaProdutos.FindAsync(id);
            if (marcaProduto == null)
            {
                return NotFound();
            }
            return View(marcaProduto);
        }

        // POST: MarcaProdutoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMarca,NomeMarca")] MarcaProduto marcaProduto)
        {
            if (id != marcaProduto.IdMarca)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(marcaProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaProdutoExists(marcaProduto.IdMarca))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(marcaProduto);
        }

        // GET: MarcaProdutoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaProduto = await _context.MarcaProdutos
                .FirstOrDefaultAsync(m => m.IdMarca == id);
            if (marcaProduto == null)
            {
                return NotFound();
            }

            return View(marcaProduto);
        }

        // POST: MarcaProdutoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marcaProduto = await _context.MarcaProdutos.FindAsync(id);
            if (marcaProduto != null)
            {
                _context.MarcaProdutos.Remove(marcaProduto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaProdutoExists(int id)
        {
            return _context.MarcaProdutos.Any(e => e.IdMarca == id);
        }
    }
}
