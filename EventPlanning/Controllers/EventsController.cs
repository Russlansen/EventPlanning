using EventPlanning.Models;
using EventPlanning.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using System.Data.Entity;

namespace EventPlanning.Controllers
{
    public class EventsController : Controller
    {
        [Authorize]
        public ActionResult AddEvent()
        {
            var model = new AddEventViewModel();
            model.OwnerName = HttpContext.User.Identity.Name;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEvent(AddEventViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
                ModelState.AddModelError("Name", "Обязательное поле");
            if (ModelState.IsValid)
            {
                var db = new AppDbContext();
                var _event = new Event(model);
                db.Events.Add(_event);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [Authorize]
        public ActionResult RegisterForTheEvent(int? eventId, string message = "")
        {
            if (eventId != null)
            {
                var user = HttpContext.User.Identity.Name;
                var db = new AppDbContext();
                var model = new RegisterForTheEventViewModel();
                model._Event = db.Events.Where(x => x.Id == eventId).FirstOrDefault();
                model._User = db.Users.Where(x => x.UserName == user).FirstOrDefault();
                ViewBag.Message = message;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult RegisterForTheEvent(RegisterForTheEventViewModel model)
        {
            if (String.IsNullOrEmpty(model._User.Email))
                ModelState.AddModelError("_User.Email", "Введите E-Mail");
            if (ModelState.IsValid)
            {
                var from = new MailAddress("tt9713283@gmail.com", "Event Planner");
                var to = new MailAddress(model._User.Email);
                var message = new MailMessage(from, to);
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("tt9713283@gmail.com", "AA123456789");
                smtp.EnableSsl = true;
                message.Subject = "Регистрация на мероприятие";
                message.IsBodyHtml = true;

                var linkBuilder = new StringBuilder();
                linkBuilder.AppendFormat("<a href =\"{0}://", HttpContext.Request.Url.Scheme);
                linkBuilder.Append(HttpContext.Request.Url.Authority);
                linkBuilder.AppendFormat("{0}\">ссылке</a>", Url.Action("ConfirmRegistration", "Events",
                                                            new { email = model._User.Email,
                                                                  name = model._User.UserName,
                                                                  eventId = model._Event.Id }));
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Подтвердите регистрацию на мероприятие {0}", model._Event.Name);
                messageBuilder.AppendFormat(" перейдя по этой {0}", linkBuilder.ToString());
                message.Body = messageBuilder.ToString();
                smtp.Send(message);
                var successMsg = "Сообщение отправлено";
                return RedirectToAction("RegisterForTheEvent", "Events", 
                                            new { eventId = model._Event.Id, message = successMsg });
            }
            return View(model);
        }

        [Authorize]
        public ActionResult ConfirmRegistration(string email, string name, int? eventId)
        {
            if(!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(name) && eventId != null)
            {
                var db = new AppDbContext();
                var _event = db.Events.Include(x => x.Users).Where(x => x.Id == eventId).FirstOrDefault();
                var _user = db.Users.Where(x => x.UserName == name).FirstOrDefault();
                _event.Users.Add(_user);
                db.SaveChanges();
                var successMsg = "Вы успешно зарегистрировались";
                return RedirectToAction("RegisterForTheEvent", "Events",
                                                new { eventId = eventId, message = successMsg });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}