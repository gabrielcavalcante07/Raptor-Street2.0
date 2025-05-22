using Microsoft.EntityFrameworkCore;
using RaptorStreet.Models;

namespace RaptorStreet.Data
{
    public class RaptorDBContext : DbContext
    {
        public RaptorDBContext(DbContextOptions<RaptorDBContext> options) : base(options) { }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Adm> Adms { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<ClienteEndereco> ClienteEnderecos { get; set; }
        public DbSet<MarcaProduto> MarcaProdutos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ClienteFav> ClienteFavs { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapear nomes das tabelas
            modelBuilder.Entity<Endereco>().ToTable("tbEnderecos");
            modelBuilder.Entity<Adm>().ToTable("tbAdm");
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<Login>().ToTable("tbLogin");
            modelBuilder.Entity<ClienteEndereco>().ToTable("tbClienteEnderecos");
            modelBuilder.Entity<MarcaProduto>().ToTable("tbMarcaProduto");
            modelBuilder.Entity<Produto>().ToTable("tbProdutos");
            modelBuilder.Entity<ClienteFav>().ToTable("tbClienteFav");
            modelBuilder.Entity<Pagamento>().ToTable("tbPagamentos");
            modelBuilder.Entity<Pedido>().ToTable("tbPedido");
            modelBuilder.Entity<ItemPedido>().ToTable("tbItemPedido");

            // Chaves primárias
            modelBuilder.Entity<Endereco>().HasKey(e => e.IdEndereco);
            modelBuilder.Entity<Adm>().HasKey(a => a.IdAdm);
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdCliente);
            modelBuilder.Entity<Login>().HasKey(l => l.IdLogin);
            modelBuilder.Entity<MarcaProduto>().HasKey(m => m.IdMarca);
            modelBuilder.Entity<Produto>().HasKey(p => p.IdProduto);
            modelBuilder.Entity<ClienteFav>().HasKey(cf => cf.IdClienteFav);
            modelBuilder.Entity<Pagamento>().HasKey(p => p.IdPag);
            modelBuilder.Entity<Pedido>().HasKey(p => p.IdPedido);
            modelBuilder.Entity<ItemPedido>().HasKey(ip => ip.IdProdutoPedido);
            // Definição de chaves primarias COMPOSTAS
            modelBuilder.Entity<ClienteEndereco>()
           .HasKey(ce => new { ce.IdEndCliente, ce.IdEnd });


            // Relacionamentos Login
            modelBuilder.Entity<Login>()
                .HasOne(l => l.Clientes)
                .WithMany(c => c.Logins)
                .HasForeignKey(l => l.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Login>()
                .HasOne(l => l.Adms)
                .WithMany(a => a.Logins)
                .HasForeignKey(l => l.IdAdm)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamentos ClienteEndereco
            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(ce => ce.Clientes)
                .WithMany(c => c.ClienteEnderecos)
                .HasForeignKey(ce => ce.Fk_IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(ce => ce.Enderecos)
                .WithMany(e => e.ClienteEnderecos)
                .HasForeignKey(ce => ce.IdEnd)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamentos ClienteFav
            modelBuilder.Entity<ClienteFav>()
                .HasOne(cf => cf.Clientes)
                .WithMany(c => c.ClienteFavs)
                .HasForeignKey(cf => cf.IdCliente);

            modelBuilder.Entity<ClienteFav>()
                .HasOne(cf => cf.Produtos)
                .WithMany(p => p.ClienteFavs)
                .HasForeignKey(cf => cf.IdProduto);

            modelBuilder.Entity<ClienteFav>()
                .HasIndex(cf => new { cf.IdCliente, cf.IdProduto })
                .IsUnique();

            // Relacionamento Produto -> Marca
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.MarcaProdutos)
                .WithMany(m => m.Produtos)
                .HasForeignKey(p => p.Fk_IdMarca);

            // Relacionamentos Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Clientes)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.Fk_IdCliente);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Pagamentos)
                .WithMany(pg => pg.Pedidos)
                .HasForeignKey(p => p.Fk_IdPag);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Enderecos)
                .WithMany(e => e.Pedidos)
                .HasForeignKey(p => p.Fk_IdEndereco);

            // Relacionamento ItemPedido
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Pedidos)
                .WithMany(p => p.ItemPedidos)
                .HasForeignKey(ip => ip.Fk_IdPedido);

            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Produtos)
                .WithMany(p => p.ItemPedidos)
                .HasForeignKey(ip => ip.Fk_IdProduto);
        }
    }
}
