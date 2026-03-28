using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Base;
using WebStore.Mapping;
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
                .ToView()!
            });


        }
        public IActionResult Cart() => View();
        public IActionResult DetailedInfo(int ProductID)
        {
            var product = pd.GetProductById(ProductID);
            if (product is null) return NotFound();
            return View(product.ToView());
        } 
    }
}
