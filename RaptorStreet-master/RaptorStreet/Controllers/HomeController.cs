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

        public IActionResult CrudAdm()
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

        public IActionResult FinalizarCompra()
        {
            /*List<Produto> produtosDoBanco = _context.Produtos.ToList();
            ViewBag.ProdutosDoBanco = produtosDoBanco;*/
            return View();
        }

        // Método para calcular a distância de Levenshtein
        public static int CalculateLevenshtein(string source, string target)
        {
            int n = source.Length;
            int m = target.Length;
            var dp = new int[n + 1, m + 1];

            for (int i = 0; i <= n; dp[i, 0] = i++) ;
            for (int j = 0; j <= m; dp[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                }
            }

            return dp[n, m];
        }

        public IActionResult Pesquisar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return RedirectToAction("Index"); // Volta para a página inicial se a pesquisa estiver vazia
            }

            var todosProdutos = _context.Produtos.ToList();

            // Filtra produtos com nomes que contenham o termo OU sejam similares (Levenshtein < 5)
            var produtosRelevantes = todosProdutos
                .Where(p =>
                    p.NomeProduto.ToLower().Contains(nome.ToLower()) ||
                    CalculateLevenshtein(p.NomeProduto.ToLower(), nome.ToLower()) < 5
                )
                .OrderBy(p => CalculateLevenshtein(p.NomeProduto.ToLower(), nome.ToLower())) // Ordena por similaridade
                .ToList();

            ViewBag.TermoPesquisa = nome;
            return View("ProdutosSimilares", produtosRelevantes); // Sempre retorna a View de resultados
        }




    }
}
