using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        [DataType(DataType.Date)]
        //public DateTime DateOfBirth { get => _DateOfBirth.Date; set => _DateOfBirth = value; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get => Convert.ToInt32((DateTime.Today - DateOfBirth).TotalDays / 365); }
        public override string ToString() => $"{Id}, {Name}, {Position}, {DateOfBirth}";
        
    }
}
