using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class RegisterUserViewModel: UserViewModel
    {
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
    }
}
