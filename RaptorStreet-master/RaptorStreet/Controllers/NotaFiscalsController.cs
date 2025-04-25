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
    public class NotaFiscalsController : Controller
    {
        private readonly RaptorDBContext _context;

        public NotaFiscalsController(RaptorDBContext context)
        {
            _context = context;
        }

        // GET: NotaFiscals
        public async Task<IActionResult> Index()
        {
            return View(await _context.NotaFiscals.ToListAsync());
        }

        // GET: NotaFiscals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = await _context.NotaFiscals
                .FirstOrDefaultAsync(m => m.IdNota == id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // GET: NotaFiscals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotaFiscals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNota,dataNF,valorNF")] NotaFiscal notaFiscal)
        {
            
                _context.Add(notaFiscal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(notaFiscal);
        }

        // GET: NotaFiscals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = await _context.NotaFiscals.FindAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }
            return View(notaFiscal);
        }

        // POST: NotaFiscals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNota,dataNF,valorNF")] NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.IdNota)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(notaFiscal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaFiscalExists(notaFiscal.IdNota))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(notaFiscal);
        }

        // GET: NotaFiscals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = await _context.NotaFiscals
                .FirstOrDefaultAsync(m => m.IdNota == id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // POST: NotaFiscals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notaFiscal = await _context.NotaFiscals.FindAsync(id);
            if (notaFiscal != null)
            {
                _context.NotaFiscals.Remove(notaFiscal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaFiscalExists(int id)
        {
            return _context.NotaFiscals.Any(e => e.IdNota == id);
        }
    }
}
