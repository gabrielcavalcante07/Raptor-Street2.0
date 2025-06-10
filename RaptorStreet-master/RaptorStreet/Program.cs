using RaptorStreet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using RaptorStreet.Repositorio.Interface;
using RaptorStreet.Repositorio;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RaptorDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DBRaptor"),
        new MySqlServerVersion(new Version(8, 0, 31))
    )
);


// Autenticação com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
    });

// Acesso ao contexto HTTP
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<RaptorStreet.Cookie.Cookie>();
builder.Services.AddScoped<RaptorStreet.CarrinhoCompra.CookieCarrinhoCompra>();

// Sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// MVC
builder.Services.AddControllersWithViews();

// Repositórios
builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
