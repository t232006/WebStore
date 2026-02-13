using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Base;
using WebStore.Domain.Identity;

namespace WebStore.ViewModels
{
    public class CommentViewModel
    {
        public int ID { get; set; }
        public string Text { get; set; } = null!;
        public User Author { get; set; }
        public int? BlogID { get; set; }
        public DateTime PublicDate { get; set; }
        public string pubDate { get => PublicDate.Date.ToString("dd.MMM.yyyy"); }
        public string pubTime { get => PublicDate.ToLocalTime().ToString("h:m"); }
        public int? ParentCommentID { get; set; }
        ICollection<Comment>? ChildComments { get; set; } = new HashSet<Comment>();
    }
}
