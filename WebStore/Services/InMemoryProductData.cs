using WebStore.Data;
using WebStore.Domain.Base;
using WebStore.Servises.Interfaces;

namespace WebStore.Services
{
    public class InMemoryProductData : IProductData
    {

        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Product> GetProduct(ProductFilter? filter)
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

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }
    }
}

