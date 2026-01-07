using WebStore.Services;
using WebStore.Services.Interfaces;
using WebStore.Servises;
using WebStore.Servises.Interfaces;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddScoped<IStaffData, InMemoryStaffData>();
            builder.Services.AddScoped<IProductData, InMemoryProductData>();
            builder.Services.AddScoped<IBlogs, InMemoryBlogs>();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();
            if (app.Environment.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseRouting();
            app.MapGet("/greetings", () => app.Configuration["ServerGreetings"]);

            //app.MapDefaultControllerRoute();
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
