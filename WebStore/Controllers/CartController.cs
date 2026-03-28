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
        public IActionResult Add(int ProductID)
        {
            _cart.Add(ProductID);
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Decrement(int ProductID)
        {
            _cart.Decrement(ProductID);
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Remove(int ProductID)
        {
            _cart.Remove(ProductID);
            return RedirectToAction("Index", "Cart");
        }
    }
}
