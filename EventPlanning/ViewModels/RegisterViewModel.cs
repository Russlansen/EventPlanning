using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EventPlanning.ViewModels
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [Display(Name = "Введите логин")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Некорректный Email адрес")]
        [Display(Name = "Введите E-Mail")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [MinLength(6, ErrorMessage = "Пароль не должен быть короче 6-ти символов")]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повторите пароль")]
        public string PasswordConfirm { get; set; }

    }
}