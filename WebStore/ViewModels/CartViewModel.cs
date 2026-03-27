namespace WebStore.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }
        public decimal TotalPrice(int ProductID) => Items.Sum(pr => pr.Quantity * pr.Product.Price);
        public int ItemsCount => Items.Sum(item => item.Quantity);

    }
}
