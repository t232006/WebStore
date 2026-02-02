using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class RegisterUserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Обязательное полеeeeeeeee")]
        [MaxLength(255)]
        public string login { get; set; } = null!;

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [MinLength(3)]
        public string password { get; set; } = null!;

        [Display(Name = "Пароль повторно")]
        [Required(ErrorMessage = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare(nameof(password),ErrorMessage ="Пароли не совпадают")]
        public string passwordConfirmation { get; set; } = null!;

        [Display(Name = "Имя")]
        public string? UserName { get; set; } = null!;

        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Не похоже на эл.почту")]
        public string e_mail { get; set; } = null!;

        [HiddenInput(DisplayValue = false)]
        public DateTime regDate { get; set; }
    }
}
