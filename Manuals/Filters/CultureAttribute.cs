using Manuals.Models;
using Manuals.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Manuals.Filters
{
    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        private UserRepository userRepository;

        public CultureAttribute()
        {
            userRepository = new UserRepository(new ApplicationDbContext());
        }


        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string cultureName = null;
            // Получаем куки из контекста, которые могут содержать установленную культуру
            //HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            //if (cultureCookie != null)
            //    cultureName = cultureCookie.Value;
            //else
            //    cultureName = "ru";
            string UserId = filterContext.HttpContext.User.Identity.GetUserId();
            if (UserId != null)
            {
                ApplicationUser user = userRepository.GetById(UserId);
                cultureName = user.Language;
                // Список культур
                List<string> cultures = new List<string>() { "ru", "en" };
                if (!cultures.Contains(cultureName))
                {
                    cultureName = "en";
                }
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //не реализован
        }

        private string GetLangFromDB(ActionExecutedContext filterContext)
        {
            string UserId = filterContext.HttpContext.User.Identity.GetUserId();
            ApplicationUser user = userRepository.GetById(UserId);
            return user.Language;
        }

        private string GetLangFromCookie(ActionExecutedContext filterContext)
        {
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                return cultureCookie.Value;
            else
                return "en";
        }
    }
}