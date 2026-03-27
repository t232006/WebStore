using WebStore.Data;
using WebStore.Domain.Base;
using WebStore.Servises.Interfaces;

namespace WebStore.Services.InMemory
{
    [Obsolete("Use InSQLProductData instead")]
    public class InMemoryProductData : IProductData
    {
        public Brand? GetBrandById(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public Product? GetProductById(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts(ProductFilter? filter)
        {
            var result = TestData.Products;
            if(filter != null)
            {
                if (filter.BrandID != null)
                    result = result.Where(p => p.BrandId == filter.BrandID).OrderBy(p => p.Order);
                if (filter.SectionID != null)
                    result = result.Where(p => p.SectionId == filter.SectionID).OrderBy(p => p.Order);
            }
            return result;
        }

        public Section? GetSectionById(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }
    }
}

