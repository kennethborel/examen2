using Microsoft.EntityFrameworkCore; // Importa Entity Framework Core
using examen.Data; // Importa tu contexto de datos (AppDbContext)

var builder = WebApplication.CreateBuilder(args);

// Agregar AppDbContext al contenedor de servicios
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Conecta con la cadena de conexión

// Agregar controladores con vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Configurar la ruta predeterminada
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
