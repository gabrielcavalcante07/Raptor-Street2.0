using Newtonsoft.Json;
using RaptorStreet.Models;

namespace RaptorStreet.CarrinhoCompra
{
    public class CookieCarrinhoCompra
    {
        private string Key = "Carrinho.Compras";
        private Cookie.Cookie _cookie;

        public CookieCarrinhoCompra(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }

        public void Salvar(List<Produto> lista)
        {
            string valor = JsonConvert.SerializeObject(lista);
            _cookie.Cadastrar(Key, valor);
        }

        public List<Produto> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<Produto>>(valor) ?? new List<Produto>();
            }
            else
            {
                return new List<Produto>();
            }
        }

        public void Cadastrar(Produto item)
        {
            List<Produto> lista = _cookie.Existe(Key) ? Consultar() : new List<Produto>();

            var itemLocalizado = lista.SingleOrDefault(a => a.IdProduto == item.IdProduto);

            if (itemLocalizado == null)
            {
                item.QuantidadeProd = 1;
                lista.Add(item);
            }
            else
            {
                itemLocalizado.QuantidadeProd += 1;
            }

            Salvar(lista);
        }

        public void Atualizar(Produto item)
        {
            var lista = Consultar();
            var itemLocalizado = lista.SingleOrDefault(a => a.IdProduto == item.IdProduto);

            if (itemLocalizado != null)
            {
                itemLocalizado.QuantidadeProd = item.QuantidadeProd;
                Salvar(lista);
            }
        }

        public void Remover(Produto item)
        {
            var lista = Consultar();
            var itemLocalizado = lista.SingleOrDefault(a => a.IdProduto == item.IdProduto);

            if (itemLocalizado != null)
            {
                lista.Remove(itemLocalizado);
                Salvar(lista);
            }
        }

        public void DiminuirProduto(Produto item)
        {
            var lista = Consultar();
            var itemLocalizado = lista.SingleOrDefault(a => a.IdProduto == item.IdProduto);

            if (itemLocalizado != null)
            {
                if (itemLocalizado.QuantidadeProd > 1)
                {
                    itemLocalizado.QuantidadeProd -= 1;
                    Salvar(lista);
                }
                else
                {
                    // Se ficar 0, remove o produto
                    lista.Remove(itemLocalizado);
                    Salvar(lista);
                }
            }
        }

        public bool Existe(string key)
        {
            return _cookie.Existe(key);
        }

        public void RemoverTodos()
        {
            _cookie.Remover(Key);
        }
    }
}
