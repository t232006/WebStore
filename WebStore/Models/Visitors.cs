namespace WebStore.Models
{
    public class Visitors
    {
        public int ID { get; set; }
        public string login { get; set; } = null!;
        public string password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string e_mail { get; set; } = null!;
        public DateTime regData { get; set; }
    }
}
