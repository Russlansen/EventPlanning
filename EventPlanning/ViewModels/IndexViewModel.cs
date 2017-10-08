using EventPlanning.Models;
using System.Collections.Generic;

namespace EventPlanning.ViewModels
{
    public class IndexViewModel
    {
        public string Login { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<Event> Events { get; set; }
        public List<User> Users { get; set; }
    }
}