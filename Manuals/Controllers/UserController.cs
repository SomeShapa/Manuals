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

        public ActionResult EditUserProfile()
        {
            var currentUserId = User.Identity.GetUserId();
            ApplicationUser userProfile = userRepository.GetById(currentUserId);
            return View(userProfile);
        }

        [HttpGet]
        public ActionResult GetUserManuals(string ID)
        {
    
            IEnumerable<ManualViewModel> manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(X=>X.UserId==ID));
            return Json(manuals, JsonRequestBehavior.AllowGet);
        }
    }
}