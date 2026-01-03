using WebStore.Domain.Base;

namespace WebStore.Servises.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProduct(ProductFilter? filter);
    }
}
