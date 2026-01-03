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

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }
    }
}

