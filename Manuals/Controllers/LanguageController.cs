using Manuals.Filters;
using Manuals.Models;
using Manuals.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manuals.Controllers
{
    [Culture]
    public class LanguageController : Controller
    {
        private UserRepository userRepository;

        public LanguageController()
        {
            userRepository = new UserRepository(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangeCulture(string lang)
        {
            string UserId = User.Identity.GetUserId();
            ApplicationUser user = userRepository.GetById(UserId);
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }
            user.Language = lang;
            userRepository.Update(user);
            userRepository.Save();
            // Сохраняем выбранную культуру в куки
            //HttpCookie cookie = Request.Cookies["lang"];
            //if (cookie != null)
            //    cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            //else
            //{

            //    cookie = new HttpCookie("lang");
            //    cookie.HttpOnly = false;
            //    cookie.Value = lang;
            //    cookie.Expires = DateTime.Now.AddYears(1);
            //}
            //Response.Cookies.Add(cookie);
            return Json(new { result = "Refresh" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTheme()
        {
            string UserId = User.Identity.GetUserId();
            if (UserId != null)
            {
                ApplicationUser user = userRepository.GetById(UserId);
                return Json(new { result = user.Theme }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }


        public ActionResult SaveTheme(string Theme)
        {
            string UserId = User.Identity.GetUserId();
            ApplicationUser user = userRepository.GetById(UserId);
            user.Theme = Theme;
            userRepository.Update(user);
            userRepository.Save();
            return View();
        }

    }
}