using RaptorStreet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using RaptorStreet.Repositorio.Interface;
using RaptorStreet.Repositorio;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

//  Configurar o Entity Framework Core para usar MySQL
builder.Services.AddDbContext<RaptorDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DBRaptor"),
        new MySqlServerVersion(new Version(8, 0, 31))
    )
);


// Autentica��o com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
    });

// Acesso ao contexto HTTP
builder.Services.AddHttpContextAccessor();

// Sess�o
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// MVC
builder.Services.AddControllersWithViews();

// Reposit�rios
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



app.UseAuthentication();   // Autentica��o
app.UseSession();          // Ativando a sess�o
app.UseAuthorization();    // Autoriza��o

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
