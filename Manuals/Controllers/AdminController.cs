using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manuals.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Manuals.Entities;
using Manuals.Repositories;

namespace Manuals.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext authContext;
        private UserProfileRepository userProfileRepository;
        public AdminController()
        {
            authContext = new ApplicationDbContext();
            userProfileRepository = new UserProfileRepository(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(string Id)
        {
            authContext.Users.Remove(authContext.Users.First(x => (x.Id == Id)));
            authContext.SaveChangesAsync();
            userProfileRepository.Delete(Id);
            return Json(new { success = true, Id = Id });
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            IEnumerable<ApplicationUser> Users = authContext.Users.ToList();
            return Json(Users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewUserProfile(string Id)
        {
            UserProfile model = userProfileRepository.GetById(Id);
            return View(model);
        }
    }
}