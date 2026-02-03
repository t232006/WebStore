using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)]

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(255)]
        public string UserName { get; set; } = null!;

        [Display(Name = "Имя")]
        public string? user_Name { get; set; } = null!;

        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Не похоже на эл.почту")]
        public string Email { get; set; } = null!;

        [HiddenInput(DisplayValue = false)]
        public DateTime regDate { get; set; }
    }
}
