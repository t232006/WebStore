using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel: IValidatableObject
    {
        [HiddenInput(DisplayValue =false)]
        public int Id { get; set; }
        [Display(Name ="Имя")]
        [Required(ErrorMessage="Обязательно имя")]
        [StringLength(20, MinimumLength =2, ErrorMessage="Длина должна быть от 2 до 20")]
        [RegularExpression("([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage ="Неверный ввод имени")]
        public string Name { get; set; }
        [Display(Name="Должность")]
        public string? Position { get; set; }
        //private DateTime _DateOfBirth;
        [DataType(DataType.Date)]
        //public DateTime DateOfBirth { get => _DateOfBirth.Date; set => _DateOfBirth = value; }
        public DateTime DateOfBirth { get; set; } = new DateTime(2000, 01, 01);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((DateTime.Today - DateOfBirth).TotalDays / 365 < 18)
            {
                return new[]
                {
                    new ValidationResult("Слишком молод для такой работы", new[]
                    {
                        nameof(DateOfBirth)
                    })
                };
            }
            return new[] { ValidationResult.Success! };
        }
    }
}
