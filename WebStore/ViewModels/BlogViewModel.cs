using WebStore.Domain.Base;
using WebStore.Domain.Identity;

namespace WebStore.ViewModels
{
    public class BlogViewModel
    {
        public string Name { get; set; } = null!;
        public User Author { get; set; }
        public string Block { get; set; } = null!;
        public short Rate { get; set; } = 3;
        public string? PictureUrl { get; set; }
        public int? BrandID { get; set; }
        public int? SectionID { get; set; }
        public DateTime publicDate { get; set; }
        public DateTime pubDate { get => publicDate.Date; }
        public DateTime pubTime { get => publicDate.ToLocalTime(); }
    }
}
