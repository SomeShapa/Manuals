using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manuals.Filters;
using Manuals.Entities;
using Manuals.Repositories;
using Manuals.Models;
using AutoMapper;

namespace Manuals.Controllers
{
    [Culture]
    public class UserController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly ManualRepository manualRepository;

        public UserController()
        {
            userRepository = new UserRepository(new ApplicationDbContext());
         
    }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewUserProfile(string Id)
        {
            ApplicationUser model = userRepository.GetById(Id);
            return View(model);
        }

        public ActionResult EditUserProfile(string userId, string returnUrl)
        {
            ApplicationUser user = userRepository.GetById(userId);
            ViewBag.ReturnUrl = returnUrl;
            return View(user);
        }

        [HttpPost]
        public ActionResult GetUserById(string Id)
        {
            ApplicationUser user = Mapper.Map<ApplicationUser>(userRepository.GetById(Id));
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateUser(ApplicationUser user, string returnUrl = "/")
        {
            userRepository.Update(user);
            userRepository.Save();
            return Json(new { result = "Redirect", url = returnUrl });
        }

        [HttpGet]
        public ActionResult GetUserManuals(string ID)
        {
    
            IEnumerable<ManualViewModel> manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(X=>X.UserId==ID));
            return Json(manuals, JsonRequestBehavior.AllowGet);
        }
    }
}