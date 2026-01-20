using WebStore.DAL.Context;
using WebStore.Domain.Base;
using WebStore.Servises.Interfaces;

namespace WebStore.Services.InSQL
{
    public class InSQLProductData : IProductData
    {
        private readonly WebStoreDB db;

        public InSQLProductData(WebStoreDB _db)
        {
            db = _db;
        }
        public IEnumerable<Brand> GetBrands()
        {
            return db.Brands;
        }

        public IEnumerable<Product> GetProducts(ProductFilter? filter = null)
        {
            var result = db.Products;
            if (filter != null)
            {
                if (filter.BrandID != null)
                    result = (Microsoft.EntityFrameworkCore.DbSet<Product>)result.Where(p => p.BrandId == filter.BrandID).OrderBy(p => p.Order);
                if (filter.SectionID != null)
                    result = (Microsoft.EntityFrameworkCore.DbSet<Product>)result.Where(p => p.SectionId == filter.SectionID).OrderBy(p => p.Order);
            }
            return result;
        }

        public IEnumerable<Section> GetSections()
        {
            throw new NotImplementedException();
        }
    }
}
