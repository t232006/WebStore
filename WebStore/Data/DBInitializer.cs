using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Base;

namespace WebStore.Data
{
    public class DBInitializer
    {
        private readonly WebStoreDB db;
        private readonly ILogger<DBInitializer> logger;

        public DBInitializer(WebStoreDB _db, ILogger<DBInitializer> _logger)
        {
            this.db = _db;
            this.logger = _logger;
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
            if (RemoveBefore) await EraseDB(Cancel).ConfigureAwait(false);
            logger.LogInformation("Starting migration...");
            await db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
            logger.LogInformation("Migration complete");
            var sectionPool = TestData.Sections.ToDictionary(s => s.ID);
            var brandPool = TestData.Brands.ToDictionary(b => b.ID);
            var authorPool = TestData._visitors.ToDictionary(a => a.ID);
            foreach (var tempRec in TestData.Blogs)
            {
                tempRec.Author = authorPool[tempRec.AuthorID];
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
                tempRec.AuthorID = 0;
                tempRec.ID = 0;
                tempRec.SectionID = null;
                tempRec.BrandID = null;
            }
            foreach (var tempRec in TestData._employees)
                tempRec.ID = 0;
            if (AddTestData)
            {
                logger.LogInformation("Coping from Employee...");
                if (await db.Employees.AnyAsync(Cancel).ConfigureAwait(false))
                    logger.LogInformation("There are some records");
                else
                    await Initialize<Employee>("Employees", TestData._employees);

                logger.LogInformation("Coping from Visitors...");
                if (await db.Visitors.AnyAsync(Cancel).ConfigureAwait(false))
                    logger.LogInformation("There are some records");
                else
                    await Initialize<Visitor>("Visitors", TestData._visitors);

                logger.LogInformation("Coping from Blogs...");
                if (await db.Blogs.AnyAsync(Cancel).ConfigureAwait(false))
                    logger.LogInformation("There are some records");
                 else
                    await Initialize<Blog>("Blogs", TestData.Blogs);

                logger.LogInformation("Coping from Products...");
                if (await db.Products.AnyAsync(Cancel).ConfigureAwait(false))
                    logger.LogInformation("There are some records");
                
                else
                {
                    await Initialize<Brand>("Brands", TestData.Brands);
                    await Initialize<Section>("Sections", TestData.Sections);
                    await Initialize<Product>("Products", TestData.Products);
                }
                    

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
    
    }
}
