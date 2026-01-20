using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class DBInitializer
    {
        private readonly WebStoreDB db;
        private readonly ILogger logger;

        public DBInitializer(WebStoreDB _db, ILogger _logger)
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
        public async Task InitializationDB(bool RemoveFirst, CancellationToken Cancel = default)
        {
            if (RemoveFirst) await EraseDB(Cancel).ConfigureAwait(false);
            logger.LogInformation("Starting migration...");
            await db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
            logger.LogInformation("Migration complete");
            await InitializateProducts();
            logger.LogInformation("Initialization complete");
        }
        private async Task InitializateProducts(CancellationToken Cancel = default)
        {
            logger.LogInformation("Coping from Products...");
            if (await db. Products.AnyAsync(Cancel).ConfigureAwait(false))
            {
                logger.LogInformation("There are some records");
                return;
            }
            using var transaction = await db.Database.BeginTransactionAsync(Cancel);

            logger.LogInformation("Sections copy...");
            await db.AddRangeAsync(TestData.Sections, Cancel);
            await db.Database.ExecuteSqlRawAsync("set identity_insert Sections on");
            await db.SaveChangesAsync();
            await db.Database.ExecuteSqlRawAsync("set identity_insert Sections off");
            logger.LogInformation("Sections copy complete");

            logger.LogInformation("Brands copy...");
            await db.AddRangeAsync(TestData.Brands, Cancel);
            await db.Database.ExecuteSqlRawAsync("set identity_insert Brands on");
            await db.SaveChangesAsync();
            await db.Database.ExecuteSqlRawAsync("set identity_insert Brands off");
            logger.LogInformation("Brands copy complete");

            logger.LogInformation("Products copy...");
            await db.AddRangeAsync(TestData.Products, Cancel);
            await db.Database.ExecuteSqlRawAsync("set identity_insert Products on");
            await db.SaveChangesAsync();
            await db.Database.ExecuteSqlRawAsync("set identity_insert Products off");
            logger.LogInformation("Products copy complete");
            await transaction.CommitAsync(Cancel); 
        }
    
    }
}
