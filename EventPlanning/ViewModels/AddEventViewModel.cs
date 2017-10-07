using EventPlanning.Binders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EventPlanning.ViewModels
{
    [ModelBinder(typeof(EventBinder))]
    public class AddEventViewModel
    {
        [Display(Name = "Название")]
        public string Name { get; set; }
        public Dictionary<string, Dictionary<string,string>> Theme { get; set; }
        public string OwnerName { get; set; }
        [Display(Name = "Дата проведения")]
        public DateTime? Date { get; set; }

        public AddEventViewModel()
        {
            Theme = new Dictionary<string, Dictionary<string, string>>();
        }
    }
}