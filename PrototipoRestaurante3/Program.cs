using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using PrototipoRestaurante3.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos
builder.Services.AddDbContext<restauranteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

// Agregar servicios para sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // duración de sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Usa sesión ANTES de Authorization
app.UseSession(); // Aquí activamos las sesiones
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Menu}/{action=Index}/{id?}");

app.Run();

