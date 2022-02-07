using ControleEstoque.Connection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuração de conexão com o banco de dados
var configurationString = builder.Configuration.GetConnectionString("ConexaoBD");
//Npsql Server
builder.Services.AddDbContext<ContextDB>(options => 
options.UseNpgsql(configurationString), ServiceLifetime.Scoped);


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
    pattern: "{controller=Produto}/{action=Index}/{id?}");

app.Run();
