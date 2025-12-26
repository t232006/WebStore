using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        //private DateTime _DateOfBirth;
        [DataType(DataType.Date)]
        //public DateTime DateOfBirth { get => _DateOfBirth.Date; set => _DateOfBirth = value; }
        public DateTime DateOfBirth { get; set; }

    }
}
