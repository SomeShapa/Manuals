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
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }
            SetLangFromCookie(lang);
            //SetLangFromDB(lang);

            return Json(new { result = "Refresh" }, JsonRequestBehavior.AllowGet);
        }

        private void SetLangFromDB(string lang)
        {
            if (User.Identity.IsAuthenticated)
            {
                string UserId = User.Identity.GetUserId();
                ApplicationUser user = userRepository.GetById(UserId);
                user.Language = lang;
                userRepository.Update(user);
                userRepository.Save();
            }
        }

        private void SetLangFromCookie(string lang)
        {
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
        }
    }
}