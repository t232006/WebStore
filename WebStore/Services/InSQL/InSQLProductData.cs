using Microsoft.EntityFrameworkCore;
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
            IQueryable<Product> result = db.Products.Include(p=>p.Brand).Include(p=>p.Section);
            if (filter != null)
            {
                if (filter.IDs!.Length > 0)
                    return result.Where(p => filter.IDs.Contains(p.ID));
                if (filter.BrandID.HasValue)
                    result = result.Where(p => p.BrandId == filter.BrandID).OrderBy(p => p.Order);
                if (filter.SectionID != null)
                    result = result.Where(p => p.SectionId == filter.SectionID).OrderBy(p => p.Order);
            }
            return result;
        }
        public Product? GetProductById(int ID)
        {
            return db.Products.Include(p => p.Section).Include(p => p.Brand).FirstOrDefault(p => p.ID == ID);
        }
        public Section? GetSectionById(int ID) => db.Sections.Include(p => p.Products).FirstOrDefault(p => p.ID == ID);
        public Brand? GetBrandById(int ID) => db.Brands.Include(p => p.Products).FirstOrDefault(p => p.ID == ID);

        public IEnumerable<Section> GetSections()
        {
            return db.Sections;
        }
    }
}
