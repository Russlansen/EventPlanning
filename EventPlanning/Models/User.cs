using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanning.Models
{
    public class User : IdentityUser
    {
        [Key]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        public List<Event> Events { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}