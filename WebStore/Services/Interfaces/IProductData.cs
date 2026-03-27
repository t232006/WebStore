using WebStore.Domain.Base;

namespace WebStore.Servises.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter? filter=null);
        public Product? GetProductById(int ID);
        public Section? GetSectionById(int ID);
        public Brand? GetBrandById(int ID);
    }
}
