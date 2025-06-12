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

        public IActionResult Mizuno()
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

        public IActionResult Marca(string nome)
        {
            var produtos = _context.Produtos
                .Include(p => p.MarcaProdutos)
                .Where(p => p.MarcaProdutos.NomeMarca.ToLower() == nome.ToLower())
                .ToList();

            ViewBag.TermoPesquisa = nome;

            // Redireciona para uma view com base no nome da marca
            switch (nome.ToLower())
            {
                case "nike":
                    return View("~/Views/Produtoes/Nike.cshtml", produtos);

                case "adidas":
                    return View("~/Views/Produtoes/Adidas.cshtml", produtos);
                case "vans":
                    return View("~/Views/Produtoes/Vans.cshtml", produtos);
                case "mizuno":
                    return View("~/Views/Produtoes/Mizuno.cshtml", produtos);
                case "puma":
                    return View("~/Views/Produtoes/Puma.cshtml", produtos);
                default:
                    return View();
            }
        }


        [HttpPost]
        public IActionResult Favoritar(int idProd)
        {
            // Recuperar idCliente da sessão corretamente
            int? idCliente = HttpContext.Session.GetInt32("idCliente");

            if (!idCliente.HasValue)
            {
                TempData["Login"] = "Primeiro faça o login";
                return RedirectToAction("Index", "Home");
            }

            // Verifica se já existe esse produto nos favoritos do cliente
            var favoritoExistente = _context.ClienteFavs
                .FirstOrDefault(f => f.IdCliente == idCliente.Value && f.IdCliente == idProd);

            if (favoritoExistente != null)
            {
                // Se já estiver favoritado, remove
                _context.ClienteFavs.Remove(favoritoExistente);
                _context.SaveChanges();


            }
            else
            {
                // Caso contrário, adiciona aos favoritos
                var novoFavorito = new ClienteFav
                {
                    IdCliente = idCliente.Value,
                    IdClienteFav = idCliente.Value
                };
                _context.ClienteFavs.Add(novoFavorito);


            }

            _context.SaveChanges();



            return RedirectToAction("Index", "Home");


        }

        [HttpPost]
        public IActionResult Desfavoritar(int idProd)
        {

            // Recuperar idCliente da sessão corretamente
            int? idCliente = HttpContext.Session.GetInt32("idCliente");

            if (!idCliente.HasValue)
            {
                TempData["Login"] = "Primeiro faça o login";
                return RedirectToAction("Index", "Home");
            }


            // Verifica se já existe esse produto nos favoritos do cliente
            var favoritoExistente = _context.ClienteFavs
                .FirstOrDefault(f => f.IdCliente == idCliente.Value && f.IdCliente == idProd);


            if (favoritoExistente != null)
            {
                // Se já estiver favoritado, remove
                _context.ClienteFavs.Remove(favoritoExistente);
                _context.SaveChanges();
            }

            return RedirectToAction("Favoritar", "ClienteFavoritoes");

        }



    }
}