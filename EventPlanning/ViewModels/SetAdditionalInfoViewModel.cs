using System.ComponentModel.DataAnnotations;

namespace EventPlanning.ViewModels
{
    public class SetAdditionalInfoViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Range(1, 120, ErrorMessage = "Возраст должен быть в диапазоне от 1 до 120 лет")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }
}