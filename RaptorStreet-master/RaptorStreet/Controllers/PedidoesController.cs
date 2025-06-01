using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaptorStreet.CarrinhoCompra;
using RaptorStreet.Data;
using RaptorStreet.Models;
using RaptorStreet.Repositorio;
using RaptorStreet.Repositorio.Interface;

namespace RaptorStreet.Controllers
{
    public class PedidoesController : Controller
    {
        private readonly RaptorDBContext _context;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;
        private readonly ILoginRepositorio _loginRepositorio;


        public PedidoesController(RaptorDBContext context, CookieCarrinhoCompra cookieCarrinhoCompra, ILoginRepositorio loginRepositorio)
        {
            _context = context;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
            _loginRepositorio = loginRepositorio;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var raptorDBContext = _context.Pedidos
                .Include(p => p.Clientes)
                .Include(p => p.Enderecos)
                .Include(p => p.Pagamentos)
                .Include(p => p.ItemPedidos);
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


        // ---------------------------- CARRINHO -------------------------------

        //PAGINA ADICIONAR ITEM
        [HttpGet]
        public IActionResult Carrinho()
        {
            return View(_cookieCarrinhoCompra.Consultar());
        }

        //PAGINA ADICIONAR ITEM
        [HttpPost]
        public IActionResult AdicionarItem(int id, int qtd, int tamanho)
        {
            Produto produto = _context.Produtos.Find(id);

            if (tamanho <= 0)
            {
                TempData["Error"] = "Selecione um tamanho.";
                return RedirectToAction("Produto", new { id });
            }

            if (produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new Produto()
                {
                    IdProduto = id,
                    QuantidadeProd = produto.QuantidadeProd,
                    ImagemProduto = produto.ImagemProduto,
                    NomeProduto = produto.NomeProduto,
                    PrecoProduto = produto.PrecoProduto,
                    Tamanho = tamanho,
                };

                _cookieCarrinhoCompra.Cadastrar(item);

                return RedirectToAction("Carrinho");
            }
        }
        //PAGINA DIMINUIR ITEM
        [HttpPost]
        public IActionResult DiminuirItem(int id, int tamanho)
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
                    Tamanho = tamanho,
                    QuantidadeProd = 1 // Diminuindo a quantidade em 1
                });

                // _cookieCarrinhoCompra.DiminuirProduto(item);

                return RedirectToAction("Carrinho");
            }
        }

        //PAGINA REMOVER ITEM
        [HttpPost]
        public IActionResult RemoverItem(int id, int tamanho)
        {
            _cookieCarrinhoCompra.Remover(new Produto() { IdProduto = id, Tamanho = tamanho });
            return Json(new { success = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // ---------------------------- FINALIZAR COMPRA -------------------------------
        [HttpGet]
        public IActionResult FinalizarCompra()
        {
            //pega os dados do carrinho via cookie
            var carrinho = _cookieCarrinhoCompra.Consultar();

            //se o carrinho estiver vazio
            if (!carrinho.Any())
            {
                TempData["Error"] = "O carrinho está vazio";
                return RedirectToAction("Carrinho");
            }

            //consulta os dados do cliente
            int? idCliente = HttpContext.Session.GetInt32("IdCliente");

            if (idCliente == null)
            {
                TempData["Error"] = "Você precisa estar logado";
                return RedirectToAction("Login", "Logins");
            }

            var cliente = _loginRepositorio.ObterCliente(idCliente.Value);

            //prepara a view
            var viewModel = new ResumoPedido
            {
                Cliente = cliente,
                Produtos = carrinho,
                Total = carrinho.Sum(p => p.PrecoProduto * p.QuantidadeProd)
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ConfirmarPedido(int FK_IdPag)
        {
            int? idCliente = HttpContext.Session.GetInt32("IdCliente");
            if (idCliente == null)
            {
                TempData["Error"] = "Você precisa estar logado";
                return RedirectToAction("Login", "Logins");
            }

            var produtosCarrinho = _cookieCarrinhoCompra.Consultar();
            if (!produtosCarrinho.Any())
            {
                TempData["Error"] = "O carrinho está vazio";
                return RedirectToAction("Carrinho");
            }

            //pegando os dados do endereco, pagamento e marca
            var enderecoId = _context.ClienteEnderecos
            .Where(e => e.Fk_IdCliente == idCliente.Value)
            .Select(e => e.IdEnd)
            .FirstOrDefault();
            
/*            var MarcaId = _context.ItemPedidos
            .Include(ip => ip.Produtos)
            .ThenInclude(p => p.MarcaProdutos)
            .Where(ip => ip.IdProdutoPedido == idCliente.Value)
            .Select(ip => ip.Produtos.MarcaProdutos.NomeMarca)
            .FirstOrDefault();*/

            var pagamentoId = 1; // exemplo fixo ou obtido via form

            var pedido = new Pedido
            {
                Fk_IdCliente = idCliente.Value,
                Fk_IdEndereco = enderecoId,
                Fk_IdPag = pagamentoId,
                dataPed = DateTime.Now,
                totalPedido = produtosCarrinho.Sum(p => p.PrecoProduto * p.QuantidadeProd),
                ItemPedidos = new List<ItemPedido>()
            };

            foreach (var item in produtosCarrinho)
            {
                pedido.ItemPedidos.Add(new ItemPedido
                {
                    NomeProduto = item.NomeProduto,
                    PrecoUnitario = item.PrecoProduto,
                    QuantidadeItem = item.QuantidadeProd,
                    TamanhoItem = item.Tamanho,
                    ImagemProduto = item.ImagemProduto,
                    Fk_IdProduto = item.IdProduto,
                });
            }

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            _cookieCarrinhoCompra.RemoverTodos(); // limpa o carrinho

            return RedirectToAction("Pedidoes");
        }

    }
}