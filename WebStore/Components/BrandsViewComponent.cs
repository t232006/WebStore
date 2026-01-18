using Microsoft.AspNetCore.Mvc;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BrandsViewComponent :ViewComponent
    {
        private readonly IProductData mpd;

        public BrandsViewComponent(IProductData MPD) => mpd = MPD;
        public IViewComponentResult Invoke() 
        {
            IEnumerable<BrandsViewModel> brands = mpd.GetBrands()
                .OrderBy(b => b.Order)
                .Select(b => new BrandsViewModel
                {
                    ID = b.ID,
                    Name = b.Name
                });
            return View(brands);
        }
    }
}
