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
        public DbSet<ClienteEndereco> ClienteEnderecos { get; set; }
        public DbSet<MarcaProduto> MarcaProdutos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ClienteFav> ClienteFavs { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<ItemCarrinho> ItemCarrinhos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<NotaFiscal> NotaFiscals { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definição de tabelas
            modelBuilder.Entity<Endereco>().ToTable("tbEnderecos");
            modelBuilder.Entity<Adm>().ToTable("tbAdm");
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<ClienteEndereco>().ToTable("tbClienteEnderecos");
            modelBuilder.Entity<MarcaProduto>().ToTable("tbMarcaProduto");
            modelBuilder.Entity<Produto>().ToTable("tbProdutos");
            modelBuilder.Entity<ClienteFav>().ToTable("tbClienteFav");
            modelBuilder.Entity<Carrinho>().ToTable("tbCarrinho");
            modelBuilder.Entity<ItemCarrinho>().ToTable("tbItemCarrinho"); // Adicionado
            modelBuilder.Entity<Pagamento>().ToTable("tbPagamentos");
            modelBuilder.Entity<NotaFiscal>().ToTable("tbNotaFiscal");
            modelBuilder.Entity<Pedido>().ToTable("tbPedido");
            modelBuilder.Entity<ItemPedido>().ToTable("tbItemPedido");

            // Chaves primárias
            modelBuilder.Entity<Endereco>().HasKey(e => e.IdEndereco);
            modelBuilder.Entity<Adm>().HasKey(ad => ad.IdAdm);
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdCliente);
            modelBuilder.Entity<ClienteEndereco>().HasKey(ce => ce.IdEndCliente);
            modelBuilder.Entity<MarcaProduto>().HasKey(mp => mp.IdMarca);
            modelBuilder.Entity<Produto>().HasKey(p => p.IdProduto);
            modelBuilder.Entity<ClienteFav>().HasKey(cf => cf.IdClienteFav);
            modelBuilder.Entity<Carrinho>().HasKey(v => v.IdCarrinho);
            modelBuilder.Entity<ItemCarrinho>().HasKey(ic => ic.IdItemCarrinho); // Adicionado
            modelBuilder.Entity<Pagamento>().HasKey(pg => pg.IdPag);
            modelBuilder.Entity<NotaFiscal>().HasKey(nf => nf.IdNota);
            modelBuilder.Entity<Pedido>().HasKey(pe => pe.IdPedido);
            modelBuilder.Entity<ItemPedido>().HasKey(ip => ip.IdProdutoPedido);

            // Relacionamentos
            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(ce => ce.Clientes)
                .WithMany(c => c.ClienteEnderecos)
                .HasForeignKey(ce => ce.Fk_IdCliente);

            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(ce => ce.Enderecos)
                .WithMany(e => e.ClienteEnderecos)
                .HasForeignKey(ce => ce.Fk_IdEndereco);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.MarcaProdutos)
                .WithMany(mp => mp.Produtos)
                .HasForeignKey(p => p.Fk_IdMarca)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClienteFav>()
                .HasOne(cf => cf.Clientes)
                .WithMany(c => c.ClienteFavs)
                .HasForeignKey(cf => cf.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClienteFav>()
                .HasOne(cf => cf.Produtos)
                .WithMany(p => p.ClienteFavs)
                .HasForeignKey(cf => cf.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Carrinho>()
                .HasOne(v => v.Clientes)
                .WithMany(c => c.Carrinhos)
                .HasForeignKey(v => v.Fk_IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemCarrinho>()
                .HasOne(ic => ic.Carrinho)
                .WithMany(c => c.ItemCarrinhos)
                .HasForeignKey(ic => ic.Fk_IdCarrinho)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCarrinho>()
                .HasOne(ic => ic.Produto)
                .WithMany() //Produto não precisa conhecer ItemCarrinho
                .HasForeignKey(ic => ic.Fk_IdProduto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(pe => pe.NotaFiscals)
                .WithMany(nf => nf.Pedidos)
                .HasForeignKey(pe => pe.Fk_IdNota)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(pe => pe.Enderecos)
                .WithMany(e => e.Pedidos)
                .HasForeignKey(pe => pe.Fk_IdEndereco)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(pe => pe.Pagamentos)
                .WithMany(pg => pg.Pedidos)
                .HasForeignKey(pe => pe.Fk_IdPag)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(pe => pe.Clientes)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(pe => pe.Fk_IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Pedidos)
                .WithMany(pe => pe.ItemPedidos)
                .HasForeignKey(ip => ip.Fk_IdPedido)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Produtos)
                .WithMany(p => p.ItemPedidos)
                .HasForeignKey(ip => ip.Fk_IdProduto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
