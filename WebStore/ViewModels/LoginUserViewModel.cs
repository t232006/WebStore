using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class LoginUserViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(255)]
        public string login { get; set; } = null!;

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [MinLength(3)]
        public string password { get; set; } = null!;

        [HiddenInput(DisplayValue = false)]
        public string? redirectUrl { get; set; }
        public bool rememberMe { get; set; }
    }
}
