using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Base;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController:Controller
    {
        private readonly IProductData pd;

        public CatalogController(IProductData PD) => pd = PD;
        public IActionResult Index([Bind("BrandID, SectionID")] ProductFilter filter) 
        {
            var products = pd.GetProducts(filter);
            return View(new CatalogViewModel
            {
                BrandID = filter.BrandID,
                SectionID = filter.SectionID,
                Products = products
                .OrderBy(p=>p.Order)
                .Select(p=>new ProductViewModel
                {
                    ID=p.ID,
                    Name=p.Name,
                    Price=p.Price,
                    PictureUrl=p.ImageUrl
                })
            });


        }
        public IActionResult Cart() => View();
    }
}
