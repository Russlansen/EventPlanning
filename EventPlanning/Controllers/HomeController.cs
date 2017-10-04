using EventPlanning.Models;
using EventPlanning.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanning.Controllers
{
    public class HomeController : Controller
    {
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        public ActionResult Index()
        {
            var model = new IndexViewModel();
            model.Login = HttpContext.User.Identity.Name;
            model.IsAuth = HttpContext.User.Identity.IsAuthenticated;
            return View(model);
        }
    }
}