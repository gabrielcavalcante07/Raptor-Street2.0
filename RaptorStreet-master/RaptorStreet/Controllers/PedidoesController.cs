using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaptorStreet.CarrinhoCompra;
using RaptorStreet.Data;
using RaptorStreet.Models;

namespace RaptorStreet.Controllers
{
    public class PedidoesController : Controller
    {
        private readonly RaptorDBContext _context;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;

        public PedidoesController(RaptorDBContext context, CookieCarrinhoCompra cookieCarrinhoCompra)
        {
            _context = context;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var raptorDBContext = _context.Pedidos.Include(p => p.Clientes).Include(p => p.Enderecos).Include(p => p.Pagamentos);
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
            ViewData["Fk_IdPag"] = new SelectList(_context.Pagamentos, "IdPag", "IdPag");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,Fk_IdEndereco,Fk_IdPag,Fk_IdCliente,dataPed,totalPedido")] Pedido pedido)
        {

            _context.Add(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            ViewData["Fk_IdPag"] = new SelectList(_context.Pagamentos, "IdPag", "IdPag", pedido.Fk_IdPag);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,Fk_IdEndereco,Fk_IdPag,Fk_IdCliente,dataPed,totalPedido")] Pedido pedido)
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




        //PAGINA CARRINHO
        [HttpGet]
        public IActionResult Carrinho()
        {
            List<ItemPedido> produtosDoBanco = _context.ItemPedidos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;
            return View(_cookieCarrinhoCompra.Consultar());
        }

        //PAGINA ADICIONAR ITEM
        [HttpPost]
        public IActionResult AdicionarItem(int id, int qtd, int tamanho)
        {
            List<ItemPedido> produtosDoBanco = _context.ItemPedidos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;
            var idCliente = HttpContext.Session.GetInt32("IdCliente");

            if (idCliente == null)
            {
                TempData["Login"] = "É necessário estar logado para adicionar ao Carrinho";
                return RedirectToAction("Index", "Home");

            }

            else
            {
                ItemPedido itemPedido = _context.ItemPedidos.Find(id);

                if (itemPedido == null)
                {
                    return View("NaoExisteItem");
                }
                else
                {
                    var item = new Produto()
                    {
                        IdProduto = id,
                        QuantidadeProd = itemPedido.Quantidade,
                        ImagemProduto = itemPedido.ImagemProduto,
                        NomeProduto = itemPedido.NomeProduto,
                        PrecoProduto = itemPedido.PrecoUnitario,
                    };

                    _cookieCarrinhoCompra.Cadastrar(item);

                    return RedirectToAction("Carrinho");
                }
            }
        }
        //PAGINA DIMINUIR ITEM
        [HttpPost]
        public IActionResult DiminuirItem(int id)
        {
            Produto produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                // Passa o ID e a quantidade reduzida diretamente
                _cookieCarrinhoCompra.DiminuirProduto(new Produto()
                {
                    IdProduto = id,
                    QuantidadeProd = 1 // Diminuindo a quantidade em 1
                });

                // _cookieCarrinhoCompra.DiminuirProduto(item);

                return RedirectToAction("Carrinho");
            }
        }

        //PAGINA REMOVER ITEM
        [HttpPost]
        public IActionResult RemoverItem(int id)
        {
            _cookieCarrinhoCompra.Remover(new Produto() { IdProduto = id });
            return Json(new { success = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}