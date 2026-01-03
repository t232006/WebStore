namespace WebStore.ViewModels
{
    public class CatalogViewModel
    {
        public int? SectionID { get; set; }
        public int? BrandID { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
    }
}
