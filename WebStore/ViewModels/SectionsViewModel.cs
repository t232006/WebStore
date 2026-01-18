namespace WebStore.ViewModels
{
    public class SectionsViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public List<SectionsViewModel> ChildSection { get; set; } = new List<SectionsViewModel>();
    }
}
