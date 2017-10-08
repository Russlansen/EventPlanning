using System.ComponentModel.DataAnnotations;

namespace EventPlanning.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите логин")]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}