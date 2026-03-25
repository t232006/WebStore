using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebStore.DAL.Context;
using WebStore.Domain.Base;
using WebStore.Domain.Identity;

namespace WebStore.Data
{
    public class DBInitializer
    {
        private readonly WebStoreDB db;
        private readonly ILogger<DBInitializer> logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public DBInitializer(WebStoreDB _db, 
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            ILogger<DBInitializer> _logger)
        {
            this.db = _db;
            this.logger = _logger;
            userManager = UserManager;
            roleManager = RoleManager;
        }
        public async Task<bool> EraseDB(CancellationToken Cancel=default)
        {
            logger.LogInformation("Erasing database...");
            var result= await db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);
            if (result)
                logger.LogInformation("Erasing complete");
            else
                logger.LogInformation("Erasing failed");
            return result;
        }
        public async Task InitializationDB(bool RemoveBefore, bool AddTestData, CancellationToken Cancel = default)
        {
            string UserID;
            if (RemoveBefore) await EraseDB(Cancel).ConfigureAwait(false);
            logger.LogInformation("Starting migration...");
            await db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
            logger.LogInformation("Migration complete");
            if (RemoveBefore)
            {
                var hasher = new PasswordHasher<IdentityUser>();
                var iu = new IdentityUser();
                string plainPassword = "123";
                string hashedPassword = hasher.HashPassword(iu, plainPassword);
                User user = new User {
                    user_Name = "Test User",
                    UserName = "Test",
                    NormalizedUserName = "TEST",
                    Email = "test@ya.ru",
                    PasswordHash = hashedPassword
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
            await InitializeIdentityAsync(Cancel);
            if (AddTestData)
            {
                UserID = db.Users.Select(u => u.Id).First();
                var sectionPool = TestData.Sections.ToDictionary(s => s.ID);
                var brandPool = TestData.Brands.ToDictionary(b => b.ID);
                //var authorPool = TestData._visitors.ToDictionary(a => a.ID);
                var authorPool = db.Users.ToDictionary(b => b.Id);
                foreach (var tempRec in TestData.Blogs)
                {
                    tempRec.Author = authorPool[UserID];
                    if (tempRec.BrandID is not null)
                        tempRec.Brand = brandPool[tempRec.BrandID.Value];
                    if (tempRec.SectionID is not null)
                        tempRec.Section = sectionPool[tempRec.SectionID.Value];

                }

                foreach (var tempRec in TestData.Sections.Where(s => s.ParentID is not null))
                    tempRec.Parent = sectionPool[tempRec.ParentID.Value];
                foreach (var tempRec in TestData.Products)
                {
                    if (tempRec.BrandId is not null)
                        tempRec.Brand = brandPool[tempRec.BrandId.Value];
                    tempRec.Section = sectionPool[tempRec.SectionId];
                    tempRec.BrandId = null;
                    tempRec.SectionId = 0;
                    tempRec.ID = 0;
                }
                foreach (var tempRec in TestData.Sections)
                {
                    tempRec.ParentID = null;
                    tempRec.ID = 0;
                }
                foreach (var tempRec in TestData.Brands)
                    tempRec.ID = 0;
                foreach (var tempRec in TestData._visitors)
                    tempRec.ID = 0;
                foreach (var tempRec in TestData.Blogs)
                {
                    tempRec.AuthorID = UserID;
                    tempRec.ID = 0;
                    tempRec.SectionID = null;
                    tempRec.BrandID = null;
                }
                foreach (var tempRec in TestData._employees)
                    tempRec.ID = 0;

                logger.LogInformation("Coping from Employee...");
                if (await db.Employees.AnyAsync(Cancel).ConfigureAwait(false))
                    logger.LogInformation("There are some records");
                else
                    await Initialize<Employee>("Employees", TestData._employees);

                /*logger.LogInformation("Coping from Visitors...");
                if (await db.Visitors.AnyAsync(Cancel).ConfigureAwait(false))
                    logger.LogInformation("There are some records");
                else
                    await Initialize<Visitor>("Visitors", TestData._visitors);*/

                logger.LogInformation("Coping from Products...");
                if (await db.Products.AnyAsync(Cancel).ConfigureAwait(false))
                    logger.LogInformation("There are some records");
                
                else
                {
                    await Initialize<Brand>("Brands", TestData.Brands);
                    await Initialize<Section>("Sections", TestData.Sections);
                    await Initialize<Product>("Products", TestData.Products);
                }
                logger.LogInformation("Coping from Blogs...");
                if (await db.Blogs.AnyAsync(Cancel).ConfigureAwait(false))
                    logger.LogInformation("There are some records");
                else
                    await Initialize<Blog>("Blogs", TestData.Blogs);

                var user = db.Users.First();
                db.BlogUserRate.Add(new BlogUserRate { BlogID = 1, UserID = user.Id, Rate = 4 });
                db.SaveChanges();
                logger.LogInformation("Initialization complete");
            }
            
        }
        private async Task Initialize<T>(string ent, IEnumerable<T> mas, CancellationToken Cancel=default)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            logger.LogInformation("{0} copy...",ent);
            await db.AddRangeAsync(mas.Cast<object>().ToArray(), Cancel);
            await db.SaveChangesAsync();
            logger.LogInformation("{0} copy complete", ent);
            await transaction.CommitAsync();
        }
        private async Task InitializeIdentityAsync(CancellationToken Cancel)
        {
            async Task CheckRole(string roleName)
            {
                if (await roleManager.RoleExistsAsync(roleName))
                {
                    logger.LogInformation($"Роль {0} существует", roleName);
                }
                else
                {
                    logger.LogInformation($"Роль {0} не существует. Создаю...", roleName);
                    await roleManager.CreateAsync(new Role { Name = roleName });
                    logger.LogInformation($"Роль {0} создана", roleName);

                }
            }
            logger.LogInformation("Начинаю инициализацию Identity");
            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);
            if (await userManager.FindByNameAsync("Admin") is null)
            {
                logger.LogInformation($"Пользователя \"Admin\" не существует. Создаю...");
                User admin = new User { UserName = "Admin" };
                var creationResult = await userManager.CreateAsync(admin, "Admin");
                if (creationResult.Succeeded)
                {
                    logger.LogInformation("Пользователь \"Admin\" создан. Даю административные права");
                    await userManager.AddToRoleAsync(admin, Role.Administrators);
                    logger.LogInformation("Пользователь \"Admin\" получил права администратора");

                }
                else
                {
                    string errors = string.Join(", ", creationResult.Errors.Select(e=>e.Description));
                    logger.LogInformation($"Пользователя \"Admin\" не удалось создать. Ошибки:{0}",errors);
                    throw new InvalidOperationException($"Не могу создать \"Admin\" из за ошибок {errors}");
                }
            } else
                logger.LogInformation("Пользователя \"Admin\" существует");


            logger.LogInformation("Инициализация прошла успешно");
        }
    
    }
}
