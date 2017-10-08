using EventPlanning.Models;
using EventPlanning.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventPlanning.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Login, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", new { message = "Регистрация успешно завершена" });
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl, string message)
        {
            var model = new LoginViewModel();
            model.IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.returnUrl = returnUrl;
            ViewBag.Message = message;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Login, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("error", "Некорректное имя или пароль.");
                }
                else
                { 
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);

                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    if (string.IsNullOrEmpty(user.FirstName))
                    {
                        return RedirectToAction("SetAdditionalInfo", "Users");
                    }
                    else if (returnUrl != null)
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Check(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content("Обязательное поле", MediaTypeNames.Text.Plain);
            }
            else
            {
                var db = new AppDbContext();
                var checkUsers = db.Users.Where(x => x.UserName == name);
                if (checkUsers.Count() == 0)
                    return Content("Имя свободно", MediaTypeNames.Text.Plain);
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Content("Имя уже занято", MediaTypeNames.Text.Plain);
                }
            }
        }
        [Authorize]
        public ActionResult SetAdditionalInfo()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult SetAdditionalInfo(SetAdditionalInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new AppDbContext();
                var name = HttpContext.User.Identity.Name;
                var user = db.Users.Where(x => x.UserName == name).FirstOrDefault();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Age = model.Age;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}