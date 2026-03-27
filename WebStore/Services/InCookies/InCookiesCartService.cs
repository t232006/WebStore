using Microsoft.CodeAnalysis;
using System.Text.Json;
using WebStore.Domain.Base;
using WebStore.Domain.Base.Interfaces;
using WebStore.Mapping;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Services.InCookies
{
    public class InCookiesCartService: ICart 
    {
        private readonly IProductData _productData;
        private readonly IHttpContextAccessor _httpConetxtAccessor;
        private readonly string _cartName;

        public Cart cart
        {
            get
            {
                var context = _httpConetxtAccessor.HttpContext!;
                var cookies = context.Response.Cookies;
                var cart_cookies = context.Request.Cookies[_cartName];
                if (cart_cookies is null)
                {
                    var _cart = new Cart();
                    cookies.Append(_cartName, JsonSerializer.Serialize(_cart));
                    return _cart;
                }
                ReplaceCart(cookies, cart_cookies); //продлить время жизни корзины
                return JsonSerializer.Deserialize<Cart>(cart_cookies)!;
            }
            set
            {
                ReplaceCart(_httpConetxtAccessor.HttpContext!.Response.Cookies, JsonSerializer.Serialize(value));
            }
        }
        void ReplaceCart(IResponseCookies cookies, string cart)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cart);
        }

        public void Add(int ProductID)
        {
            var _cart = cart;
            cart.Add(ProductID);
            cart = _cart;
        }

        public void Decriment(int ProductID)
        {
            var _cart = cart;
            cart.Decriment(ProductID);
            cart = _cart;
        }

        public void Remove(int ProductID)
        {
            var _cart = cart;
            cart.Remove(ProductID);
            cart = _cart;
        }

        public void Clear()
        {
            var _cart = cart;
            cart.Clear();
            cart = _cart;
        }

        public CartViewModel GetViewModel()
        {
            var products = _productData.GetProducts(new()
            {
                IDs = cart.Items.Select(item => item.ItemID).ToArray()
            });
            var prod_views = products.ToView().ToDictionary(k => k.ID);
            return new() { Items = cart.Items
                                                .Where(item=>prod_views.ContainsKey(item.ItemID))
                                                .Select(item => (prod_views[item.ItemID], item.Quantity))! };
        }

        public InCookiesCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _productData = productData;
            _httpConetxtAccessor = httpContextAccessor;
            var user = httpContextAccessor.HttpContext!.User;
            var userName= user.Identity!.IsAuthenticated? $"-{ user.Identity.Name}": null;
            _cartName = $"WebStore.GB.Cart{userName}";
        }
    }
}
