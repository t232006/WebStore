using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Base;

namespace WebStore.DAL.Context
{
    internal class WebStoreDB :DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public WebStoreDB (DbContextOptions<WebStoreDB> DBOptions) : base(DBOptions)
        {

        }

    }
}
