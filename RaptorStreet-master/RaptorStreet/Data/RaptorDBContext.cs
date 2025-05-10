using System.Collections.Generic;
using System.Reflection.Emit;
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
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<NotaFiscal> NotaFiscals { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir o nome correto das tabelas
            modelBuilder.Entity<Endereco>().ToTable("tbEnderecos");
            modelBuilder.Entity<Adm>().ToTable("tbAdm");
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<Login>().ToTable("tbLogin");
            modelBuilder.Entity<ClienteEndereco>().ToTable("tbClienteEnderecos");
            modelBuilder.Entity<MarcaProduto>().ToTable("tbMarcaProduto");
            modelBuilder.Entity<Produto>().ToTable("tbProdutos");
            modelBuilder.Entity<ClienteFav>().ToTable("tbClienteFav"); 
            modelBuilder.Entity<Carrinho>().ToTable("tbCarrinho");
            modelBuilder.Entity<Pagamento>().ToTable("tbPagamentos");
            modelBuilder.Entity<NotaFiscal>().ToTable("tbNotaFiscal");
            modelBuilder.Entity<Pedido>().ToTable("tbPedido");
            modelBuilder.Entity<ItemPedido>().ToTable("tbItemPedido");


            // Definição das chaves primárias
            modelBuilder.Entity<Endereco>().HasKey(e => e.IdEndereco);
            modelBuilder.Entity<Adm>().HasKey(ad => ad.IdAdm);
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdCliente);
            modelBuilder.Entity<Login>().HasKey(log => log.IdLogin);
            modelBuilder.Entity<MarcaProduto>().HasKey(mp => mp.IdMarca);
            modelBuilder.Entity<Produto>().HasKey(p => p.IdProduto);
            modelBuilder.Entity<ClienteFav>().HasKey(cf => cf.IdClienteFav);
            modelBuilder.Entity<Carrinho>().HasKey(v => v.IdCarrinho);
            modelBuilder.Entity<Pagamento>().HasKey(pg => pg.IdPag);
            modelBuilder.Entity<NotaFiscal>().HasKey(nf => nf.IdNota);
            modelBuilder.Entity<Pedido>().HasKey(pe => pe.IdPedido);
            modelBuilder.Entity<ItemPedido>().HasKey(ip => ip.IdProdutoPedido);
            // Definição de chaves primarias COMPOSTAS
            modelBuilder.Entity<ClienteEndereco>()
           .HasKey(ce => new { ce.IdEndCliente, ce.IdEnd });

            // Relacionamento ClienteEndereco -> Cliente
            modelBuilder.Entity<ClienteEndereco>()
            .HasOne(ce => ce.Clientes)
            .WithMany(c => c.ClienteEnderecos)
            .HasForeignKey(ce => ce.IdEndCliente);

            // Relacionamento ClienteEndereco -> Endereco
            modelBuilder.Entity<ClienteEndereco>()
            .HasOne(ce => ce.Enderecos)
            .WithMany(e => e.ClienteEnderecos)
            .HasForeignKey(ce => ce.IdEndCliente);

            // Relacionamento Produto -> MarcaProduto (1:N)
            modelBuilder.Entity<Produto>()
           .HasOne(p => p.MarcaProdutos)
           .WithMany(mp => mp.Produtos)
           .HasForeignKey(p => p.Fk_IdMarca)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ClienteFav -> Cliente (1:N)
            modelBuilder.Entity<ClienteFav>()
           .HasOne(cf => cf.Clientes)
           .WithMany(c => c.ClienteFavs)
           .HasForeignKey(cf => cf.IdCliente)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ClienteFav -> Produto (1:N)
            modelBuilder.Entity<ClienteFav>()
           .HasOne(cf => cf.Produtos)
           .WithMany(p => p.ClienteFavs)
           .HasForeignKey(cf => cf.IdProduto)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Venda -> Cliente (1:N)
            modelBuilder.Entity<Carrinho>()
           .HasOne(v => v.Clientes)
           .WithMany(c => c.Carrinhos)
           .HasForeignKey(v => v.Fk_IdCliente)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Pedido -> Nota Fiscal (1:N)
            modelBuilder.Entity<Pedido>()
           .HasOne(pe => pe.NotaFiscals)
           .WithMany(nf => nf.Pedidos)
           .HasForeignKey(pe => pe.Fk_IdNota)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Pedido -> Endereco (1:N)
            modelBuilder.Entity<Pedido>()
           .HasOne(pe => pe.Enderecos)
           .WithMany(e => e.Pedidos)
           .HasForeignKey(pe => pe.Fk_IdEndereco)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Pedido -> Pagamento (1:N)
            modelBuilder.Entity<Pedido>()
           .HasOne(pe => pe.Pagamentos)
           .WithMany(pg => pg.Pedidos)
           .HasForeignKey(pe => pe.Fk_IdPag)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Pedido -> Cliente (1:N)
            modelBuilder.Entity<Pedido>()
           .HasOne(pe => pe.Clientes)
           .WithMany(c => c.Pedidos)
           .HasForeignKey(pe => pe.Fk_IdCliente)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemPedido -> Pedido (1:N)
            modelBuilder.Entity<ItemPedido>()
           .HasOne(ip => ip.Pedidos)
           .WithMany(pe => pe.ItemPedidos)
           .HasForeignKey(pe => pe.Fk_IdPedido)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemPedido -> Produto (1:N)
            modelBuilder.Entity<ItemPedido>()
           .HasOne(ip => ip.Produtos)
           .WithMany(p => p.ItemPedidos)
           .HasForeignKey(pe => pe.Fk_IdProduto)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Login -> Cliente
            modelBuilder.Entity<Login>()
           .HasOne(log => log.Clientes)
           .WithMany(c => c.Logins)
           .HasForeignKey(log => log.IdCliente)
           .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Login -> Adm
            modelBuilder.Entity<Login>()
           .HasOne(log => log.Adms)
           .WithMany(ad => ad.Logins)
           .HasForeignKey(log => log.IdAdm)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
