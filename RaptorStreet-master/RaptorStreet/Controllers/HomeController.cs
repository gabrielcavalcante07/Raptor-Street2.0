using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaptorStreet.Data;
using RaptorStreet.Models;
using RaptorStreet.Repositorio;
using RaptorStreet.Repositorio.Interface;
using System.Diagnostics;

namespace RaptorStreet.Controllers
{
    public class HomeController : Controller
    {
    
        //DECLARANDO OS OBJETOS QUE SERÃO UTILIZADOS NO PROJETO
        private readonly ILogger<HomeController> _logger;
        private ILoginRepositorio? _loginRepositorio;
        private readonly RaptorDBContext _context;
        

        //CRIANDO O CONSTRUTOR COM OS OBJETOS CRIADOS
        public HomeController(ILogger<HomeController> logger, ILoginRepositorio loginRepositorio, RaptorDBContext context)
        {
            _logger = logger;
            _loginRepositorio = loginRepositorio;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Produto> produtosDoBanco = _context.Produtos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;
            return View();
        }
        public IActionResult Produto(int id)
        {
            List<Produto> produtosDoBanco = _context.Produtos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;
            var produto = _context.Produtos.FirstOrDefault(p => p.IdProduto == id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        public IActionResult Filtro()
        {
            List<Produto> produtosDoBanco = _context.Produtos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;
            return View();
        }


        public IActionResult Carrinho()
        {
            List<Produto> produtosDoBanco = _context.Produtos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;
            return View();
        }

        public IActionResult CadAdm()
        {
            List<Produto> produtosDoBanco = _context.Produtos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;
            return View();

        }

        public IActionResult TelaCompra()
        {
            List<Produto> produtosDoBanco = _context.Produtos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;
            return View();
        }

        public async Task<IActionResult> TelaVans()
        {
            var produtosVans = await _context.Produtos
                .Include(p => p.MarcaProdutos)
                .Where(p => p.MarcaProdutos.NomeMarca.ToLower() == "Vans")
                .ToListAsync();

            ViewBag.ProdutosDoBanco = produtosVans;

            return View();
        }


        public IActionResult Pesquisar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return RedirectToAction("Index"); // Volta para a página inicial se a pesquisa estiver vazia
            }

            var todosProdutos = _context.Produtos.ToList();

            ViewBag.TermoPesquisa = nome;
            return View("ProdutosSimilares", todosProdutos); // Sempre retorna a View de resultados
        }




    }
}
