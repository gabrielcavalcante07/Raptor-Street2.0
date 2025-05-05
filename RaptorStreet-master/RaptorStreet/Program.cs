using Microsoft.EntityFrameworkCore;
using RaptorStreet.Data;
using RaptorStreet.Libraries.LoginUsuarios;
using RaptorStreet.Repositorio.Interface;
using RaptorStreet.Repositorio;

var builder = WebApplication.CreateBuilder(args);

//  Configurar o Entity Framework Core para usar MySQL
builder.Services.AddDbContext<RaptorDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DBRaptor"),
        new MySqlServerVersion(new Version(8, 0, 31))
    )
);

//  Registrar todos os serviços ANTES do builder.Build()
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();
builder.Services.AddScoped<RaptorStreet.Libraries.Sessao.Sessao>();
builder.Services.AddScoped<LoginUsuarios>();

//  Build do app depois de registrar os serviços
var app = builder.Build();

// Configuração do pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
