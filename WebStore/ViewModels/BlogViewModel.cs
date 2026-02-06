using WebStore.Domain.Base;
using WebStore.Domain.Identity;

namespace WebStore.ViewModels
{
    public class BlogViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public User Author { get; set; }
        public string Block { get; set; } = null!;
        public short Rate { get; set; } = 3;
        public string? PictureUrl { get; set; }
        public int? BrandID { get; set; }
        public string ShortTextBlock { get => Block.Substring(0, 800); }
        public int? SectionID { get; set; }
        public DateTime publicDate { get; set; }
        public string pubDate { get => publicDate.Date.ToString("dd.MMM.yyyy"); }
        public string pubTime { get => publicDate.ToLocalTime().ToString("h:m"); }
    }
}
