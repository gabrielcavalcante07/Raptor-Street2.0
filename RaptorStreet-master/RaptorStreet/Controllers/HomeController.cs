using Microsoft.AspNetCore.Mvc;
using RaptorStreet.Models;
using System.Diagnostics;

namespace RaptorStreet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<string> palavrasChave = new List<string>
        { "carrinho", "adm", "crud", "home", "início", "produto", "tênis", "filtro" };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Produto()
        {
            return View();
        }

        public IActionResult Filtro()
        {
            return View();
        }

        public IActionResult CrudAdm()
        {
            return View();
        }


        public IActionResult Carrinho()
        {
            return View();
        }

        public IActionResult CadAdm()
        {
            return View();

        }

        public IActionResult TelaCompra()
        {
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

        [HttpGet]
        public IActionResult Pesquisar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return RedirectToAction("Index");
            }

            nome = nome.ToLower();

            // Calcula a distância de Levenshtein entre o nome pesquisado e as palavras-chave
            var palavraCorreta = palavrasChave
                .OrderBy(p => CalculateLevenshtein(nome, p)) // Ordena pelas menores distâncias
                .FirstOrDefault();

            int distancia = CalculateLevenshtein(nome, palavraCorreta); // Calcular a distância real

            // DEBUG - Verificar a palavra mais próxima e a distância
            Console.WriteLine($"Palavra digitada: {nome}, Palavra mais próxima: {palavraCorreta}, Distância: {distancia}");

            // Se a distância entre as palavras for pequena (<= 3 erros), faz o redirecionamento
            if (palavraCorreta != null && distancia <= 3)
            {
                return RedirecionarParaPagina(palavraCorreta);
            }

            return RedirectToAction("Index");
        }

        // Método de redirecionamento para as páginas específicas
        private IActionResult RedirecionarParaPagina(string palavraCorreta)
        {
            switch (palavraCorreta)
            {
                case "home":
                case "início":
                    return RedirectToAction("Index");
                case "filtro":
                    return RedirectToAction("Filtro");
                case "carrinho":
                    return RedirectToAction("Carrinho");
                case "adm":
                case "crud":
                case "administração":
                    return RedirectToAction("CrudAdm");
                case "produto":
                case "tênis":
                    return RedirectToAction("Produto");
                default:
                    return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
