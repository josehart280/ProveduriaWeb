using Microsoft.EntityFrameworkCore;
using ProveduriaWeb;

var builder = WebApplication.CreateBuilder(args);

// Añade la configuración
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProveeduriaPiiContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConnectionStrings:ProveduriaWebContext");
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
