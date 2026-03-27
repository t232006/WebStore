using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Controllers
{
    public class CartController:Controller
    {
        private readonly ICart _cart;

        public CartController(ICart cart)
        {
            _cart = cart;
        }
        public IActionResult Index() => View(_cart.GetViewModel());
    }
}
