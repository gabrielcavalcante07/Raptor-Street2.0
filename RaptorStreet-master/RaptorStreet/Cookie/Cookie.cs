namespace RaptorStreet.Cookie
{
    public class Cookie
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;

        public Cookie(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Cadastra os passos do cliente (produto que selecionou) e armazena na página
        public void Cadastrar(string Key, string Valor)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(7);
            Options.IsEssential = true;

            _context.HttpContext.Response.Cookies.Append(Key, Valor, Options);
        }

        //remove os coookies
        public void Remover(string Key)
        {
            _context.HttpContext.Response.Cookies.Delete(Key);
        }

        //Compara os dados já cadastrados
        public string Consultar(string Key)
        {
            var valor = _context.HttpContext.Request.Cookies[Key];
            return valor;
        }

        //Mostra os dados cadastrados 
        public bool Existe(string Key)
        {
            if (_context.HttpContext.Request.Cookies[Key] == null)
            {
                return false;
            }
            return true;
        }

        //Se a página atualizar, guarda os cookies
        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                Remover(Key);
            }
            Cadastrar(Key, Valor);
        }
    }
}
