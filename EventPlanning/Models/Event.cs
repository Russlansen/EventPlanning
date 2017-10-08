using EventPlanning.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanning.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ThemesJson { get; set; }
        [NotMapped]
        public Dictionary<string, Dictionary<string,string>> Theme{ get; set; }
        public string OwnerName { get; set; }
        public DateTime? Date { get; set; }
        public int? Limit { get; set; }
        public List<User> Users { get; set; }
        [NotMapped]
        public bool AllowRegistration { get; set; }

        public Event(){}

        public Event(AddEventViewModel model)
        {
            Name = model.Name;
            ThemesJson = JsonConvert.SerializeObject(model.Theme);
            OwnerName = model.OwnerName;
            Date = model.Date;
            Limit = model.Limit;
        }
    }
    
}