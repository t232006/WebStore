using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class VisitorsViewModel:IValidatableObject
    {
        [HiddenInput(DisplayValue =false)]
        public int ID { get; set; }
        [Display(Name="Логин")]
        [Required(ErrorMessage="Обязательное поле")]
        public string login { get; set; } = null!;
        [Display(Name="Пароль")]
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(4)]
        public string password { get; set; } = null!;
        [Display(Name = "Пароль повторно")]
        [Required(ErrorMessage = "Повторите пароль")]
        public string passwordAgain { get; set; } = null!;
        [Display(Name = "Имя")]
        public string Name { get; set; } = null!;
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",ErrorMessage ="Не похоже на эл.почту")]
        public string e_mail { get; set; } = null!;
        [HiddenInput(DisplayValue =false)]
        public DateTime regDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (password == passwordAgain) return new[] { ValidationResult.Success! };
            return new[]
            {
                new ValidationResult("Пароли не совпадают", new[]{ nameof(passwordAgain) }) 
            };
        }
    }
}
