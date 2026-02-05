using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Base;
using WebStore.Domain.Identity;

namespace WebStore.DAL.Context
{
    public class WebStoreDB :IdentityDbContext<User, Role, string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Visitor> Visitors {get;set;}
        public DbSet<Comment> Comments { get; set; }
        public WebStoreDB (DbContextOptions<WebStoreDB> DBOptions) : base(DBOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany() // ChildComments у вас не публичное свойство, поэтому без имени навигации
                .HasForeignKey(c => c.CommentID)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
