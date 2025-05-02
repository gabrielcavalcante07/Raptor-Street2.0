using Microsoft.AspNetCore.Mvc;
using RaptorStreet.Models;
using System.Diagnostics;

namespace RaptorStreet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<string> palavrasChave = new List<string>
        { "carrinho", "adm", "crud", "home", "in�cio", "produto", "t�nis", "filtro" };

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


        // M�todo para calcular a dist�ncia de Levenshtein
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

            // Calcula a dist�ncia de Levenshtein entre o nome pesquisado e as palavras-chave
            var palavraCorreta = palavrasChave
                .OrderBy(p => CalculateLevenshtein(nome, p)) // Ordena pelas menores dist�ncias
                .FirstOrDefault();

            int distancia = CalculateLevenshtein(nome, palavraCorreta); // Calcular a dist�ncia real

            // DEBUG - Verificar a palavra mais pr�xima e a dist�ncia
            Console.WriteLine($"Palavra digitada: {nome}, Palavra mais pr�xima: {palavraCorreta}, Dist�ncia: {distancia}");

            // Se a dist�ncia entre as palavras for pequena (<= 3 erros), faz o redirecionamento
            if (palavraCorreta != null && distancia <= 3)
            {
                return RedirecionarParaPagina(palavraCorreta);
            }

            return RedirectToAction("Index");
        }

        // M�todo de redirecionamento para as p�ginas espec�ficas
        private IActionResult RedirecionarParaPagina(string palavraCorreta)
        {
            switch (palavraCorreta)
            {
                case "home":
                case "in�cio":
                    return RedirectToAction("Index");
                case "filtro":
                    return RedirectToAction("Filtro");
                case "carrinho":
                    return RedirectToAction("Carrinho");
                case "adm":
                case "crud":
                case "administra��o":
                    return RedirectToAction("CrudAdm");
                case "produto":
                case "t�nis":
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
