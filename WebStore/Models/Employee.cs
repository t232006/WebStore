namespace WebStore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get => Convert.ToInt32((DateTime.Today - DateOfBirth).TotalDays/365); }
    }
}
