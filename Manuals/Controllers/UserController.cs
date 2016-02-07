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

namespace Manuals.Controllers
{
    [Culture]
    public class UserController : Controller
    {
        private readonly UserRepository userRepository;

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

        public ActionResult EditUserProfile(string userId)
        {
            ApplicationUser user = userRepository.GetById(userId);
            return View(user);
        }
    }
}