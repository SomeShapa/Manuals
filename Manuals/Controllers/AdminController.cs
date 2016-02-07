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
        private UserRepository userRepository;
        public AdminController()
        {
            authContext = new ApplicationDbContext();
            userRepository = new UserRepository(new ApplicationDbContext());
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
            userRepository.Delete(Id);
            return Json(new { success = true, Id = Id });
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            IEnumerable<ApplicationUser> Users =Mapper.Map<List<ApplicationUser>>(userRepository.GetAll());
            return Json(Users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewUserProfile(string Id)
        {
            ApplicationUser model = userRepository.GetById(Id);
            return View(model);
        }
    }
}