using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;


namespace MyWebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<Model.Interfaces.IDbConnection, Model.Implementation.DbConnection>();
            builder.Services.AddScoped<Model.Interfaces.ICityProvider, Model.Implementation.CityProvider>();

            builder.Services.AddMvc();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".MyWebSite.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();


            /*
            //app.MapGet("/", () => "Hello World!");
            app.MapGet("/test", () => "Это тест");

            app.MapGet("/param", async context =>
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("Это строка параметров:" + context.Request.QueryString.ToString());
            });

            app.MapGet("/func", HomePage);

            app.MapGet("/html", async context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("<h1>Привет,</h1>Мир!" + context.Request.QueryString.ToString());
            });

            app.MapGet("/", () => app.Configuration["HelloMessage"]);

            async Task HomePage(HttpContext context)
            {
                await context.Response.WriteAsync("HelloWorld");
            }
            */

            //app.MapDefaultControllerRoute();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();

            


        }
    }
}
