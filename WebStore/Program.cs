using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Services.InMemory;
using WebStore.Services.InSQL;
using WebStore.Services.Interfaces;
using WebStore.Servises.Interfaces;
using WebStore.Domain.Base;

namespace WebStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddScoped<IStaffData<Visitor>, InMemoryVisitorsData>();
            builder.Services.AddScoped<IStaffData<Visitor>, InSQLVisitorsData>();
            //builder.Services.AddScoped<IStaffData<Employee>, InMemoryStaffData>();
            builder.Services.AddScoped<IStaffData<Employee>, InSQLStaffData>();
            //builder.Services.AddScoped<IProductData, InMemoryProductData>();
            builder.Services.AddScoped<IProductData, InSQLProductData>();
            //builder.Services.AddScoped<IBlogs, InMemoryBlogs>();
            builder.Services.AddScoped<IBlogs, InSQLBlogs>();
            builder.Services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
            
            builder.Services.AddScoped<DBInitializer>();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var db_init = scope.ServiceProvider.GetRequiredService<DBInitializer>();
                await db_init.InitializationDB(app.Configuration.GetValue("DB_recreate", false),
                                                app.Configuration.GetValue("AddTestData", true));
            }

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
