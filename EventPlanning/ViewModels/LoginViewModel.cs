﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public bool IsAuth { get; set; }
    }
}