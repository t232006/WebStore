using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class EditUserViewModel: UserViewModel, IValidatableObject
    {
        [Display(Name ="Старый пароль")]
        [DataType(DataType.Password)]
        public string? oldPassword { get; set; } = null!;
        
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [MinLength(3)]
        public string? password { get; set; } = null!;

        [Display(Name = "Пароль повторно")]
        [DataType(DataType.Password)]
        [Compare(nameof(password),ErrorMessage ="Пароли не совпадают")]
        public string? passwordConfirmation { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!password.IsNullOrEmpty() && (oldPassword.IsNullOrEmpty())) 
                return new[]
            {
                new ValidationResult("Введите старый пароль", new[]{nameof(oldPassword)})
            };
            return new[] { ValidationResult.Success! };
        }
    }
}
