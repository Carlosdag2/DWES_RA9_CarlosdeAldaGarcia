using DWES_RA9_CarlosdeAldaGarcia.Services;

namespace DWES_RA9_CarlosdeAldaGarcia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configurar HttpClient para consumir la API REST
            // La URL base de la API se configura en appsettings.json
            builder.Services.AddHttpClient<IndicadorApiService>(client =>
            {
                // Obtener la URL de la API desde la configuración
                var apiUrl = builder.Configuration["ApiSettings:BaseUrl"] 
                    ?? "https://localhost:7097"; // URL por defecto
                
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
