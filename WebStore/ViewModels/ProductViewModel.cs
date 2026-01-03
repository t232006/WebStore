namespace WebStore.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
    }
}
