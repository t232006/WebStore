using System.Xml.Linq;
using WebStore.Domain.Base;
using WebStore.ViewModels;

namespace WebStore.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel? ToView(this Product? p) => p is null ?
            null :
            new ProductViewModel
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                PictureUrl = p.ImageUrl
            };
        public static IEnumerable<ProductViewModel?> ToView(this IEnumerable<Product?> products) => 
            products.Select(p=>p.ToView());
            

    }

}
