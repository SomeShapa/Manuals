using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manuals.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using Manuals.Filters;
using Manuals.Repositories;
using Manuals.Entities;

namespace Manuals.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext authContext;
        private readonly ManualRepository manualRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly TagRepository tagRepository;
        private readonly RatingRepository ratingRepository;
        private readonly TemplatesRepository templatesRepository;
        private readonly UserRepository userRepository;

        public HomeController()
        {
            authContext = new ApplicationDbContext();
            manualRepository = new ManualRepository(new ApplicationDbContext());
            categoryRepository = new CategoryRepository(new ApplicationDbContext());
            tagRepository = new TagRepository(new ApplicationDbContext());
            ratingRepository = new RatingRepository(new ApplicationDbContext());
            templatesRepository = new TemplatesRepository(new ApplicationDbContext());
            userRepository = new UserRepository(new ApplicationDbContext());
        }

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetManualPage(string category, int page)
        {
            IEnumerable<ManualViewModel> manuals = new List<ManualViewModel>();
            int position = manualRepository.GetAll().Count()- ((page+1) * 7);
            int a = manualRepository.GetAll().Count() - position;
            if (position<0) {a = 7 + position; position = 0; }
            if (a > 7) a = 7;
            if (a>0) { 
            if (category != "") { manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x => x.Category.Name == category)).GetRange(position, a); }
            else { manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll()).GetRange(position, a); } }
            return Json(manuals, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetManuals()
        {
            IEnumerable<ManualViewModel> manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll());
            return Json(manuals, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult CreateNewManual(string ReturnUrl)
        {
            ManualViewModel model = new ManualViewModel();
            ViewBag.ReturnUrl = ReturnUrl;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNewManual(ManualViewModel model, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.Identity.GetUserId();
                model.UserId = currentUserId;
                model.DateAdded = DateTime.Now;
               if (model.TemplateId==3) model.VideoLink = model.VideoLink.Substring(model.VideoLink.IndexOf("v=")+2, 11); 
                Manual manual = Mapper.Map<Manual>(model);
                manualRepository.Add(manual);
                manualRepository.Save();
                return Json(new { result = "Redirect", url = ReturnUrl });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteManual(ManualViewModel manual)
        {
           if (User.IsInRole("Admin") || manual.UserId == User.Identity.GetUserId())
            {
                manualRepository.Delete(manual.Id);
                manualRepository.Save();


            }

            return null;
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            IEnumerable<CategoryViewModel> categories = Mapper.Map<List<CategoryViewModel>>(categoryRepository.GetAll());
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult GetTemplates()
        {
            IEnumerable<TemplateViewModel> templates = Mapper.Map<List<TemplateViewModel>>(templatesRepository.GetAll());
            return Json(templates, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTags()
        {
            IEnumerable<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(tagRepository.GetAll());
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddNewTag(string tagName)
        {
            TagViewModel tag = CreateOrGetTag(tagName);
            return Json(new { success = true, tag = tag });
        }

        public ActionResult AutoCompleteTag(string term)
        {
            IEnumerable<string> tags = tagRepository.GetAll()
                .Where(e => e.Name.StartsWith(term) || term == null)
                .Select(e => e.Name)
                .Distinct()
                .ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeRating(ManualViewModel manual, bool liked)
        {
            string currentUserId = User.Identity.GetUserId();
            CalculateRating(manual,currentUserId, liked);
            int rating = GetRating(manual.Id);
            return Json(new { newRating = rating });
        }

        private void CalculateRating(ManualViewModel manual,string userId, bool liked)
        {
            Rating rating = ratingRepository.GetAll()
                .FirstOrDefault(e => e.UserId == userId && e.ManualId == manual.Id);
            if (rating == null)
            {
                ratingRepository.Add(new Rating
                {
                    ManualId = manual.Id,
                    UserId = userId,
                    Liked = liked
                });
            }
            else if (rating.Liked != liked)
            {
                if (rating.Liked == null)
                {
                    rating.Liked = liked;
                }
                else
                {
                    rating.Liked = null;
                }
                ratingRepository.Update(rating);
            }
            ratingRepository.Save();

        }

        private int GetRating(int manualId)
        {
            var ratings = manualRepository.GetById(manualId).Ratings;
            return (ratings.Count(e => e.Liked == true) - ratings.Count(e => e.Liked == false));
        }

        private TagViewModel CreateOrGetTag(string tagName)
        {
            Tag tagExist = tagRepository.GetAll().FirstOrDefault(t => t.Name == tagName);
            if (tagExist != null)
            {
                return Mapper.Map<TagViewModel>(tagExist);
            }
            Tag newTag = new Tag { Name = tagName };
            tagRepository.Add(Mapper.Map<Tag>(newTag));
            tagRepository.Save();
            return Mapper.Map<TagViewModel>(newTag);
        }
    }
}