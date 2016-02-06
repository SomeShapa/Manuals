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
        private readonly UserProfileRepository userProfileRepository;

        public UserController()
        {
            userProfileRepository = new UserProfileRepository(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewUserProfile(string Id)
        {
            UserProfile model = userProfileRepository.GetById(Id);
            return View(model);
        }

        public ActionResult EditUserProfile()
        {
            var currentUserId = User.Identity.GetUserId();
            UserProfile userProfile = userProfileRepository.GetById(currentUserId);
            return View(userProfile);
        }
    }
}