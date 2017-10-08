using EventPlanning.Models;
using EventPlanning.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EventPlanning.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            var db = new AppDbContext();
            model.Events = db.Events.Include(x => x.Users).ToList();      
            model.Login = HttpContext.User.Identity.Name;
            model.IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            
            foreach (var _event in model.Events)
            {
                _event.Theme = JsonConvert.DeserializeObject
                                    <Dictionary<string, Dictionary<string, string>>>(_event.ThemesJson);
            }
            return View(model);
        }
    }
}