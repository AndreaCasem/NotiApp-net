using Microsoft.EntityFrameworkCore;
using NotiApp.Models;

using NotiApp.Servicios.Contrato;
using NotiApp.Servicios.Implementacion;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NotiApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuraci�n del contexto de la base de datos para el proyecto.
builder.Services.AddDbContext<NotiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

// Permitir utilizar este servicio en todo el proyecto
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // La autenticaci�n ser� v�lida durante 20 minutos desde la �ltima interacci�n del usuario.

    });

// Borrar cach� para cuando se cierre sesi�n
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(
            new ResponseCacheAttribute
            {
                NoStore = true,
                Location = ResponseCacheLocation.None,
            }
        );
});

// Configuraci�n del Singleton DbContext.
var serviceProvider = builder.Services.BuildServiceProvider();
var dbContextSingleton = DbContextSingleton.Instance(serviceProvider);

builder.Services.AddSingleton(dbContextSingleton);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Habilitaci�n para utilizar la autenticaci�n por Cookies

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}"); // Dirigir a la vista IniciarSesion por defecto
app.Run();