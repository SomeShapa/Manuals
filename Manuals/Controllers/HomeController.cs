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

        public void MedalsCheck()
        {
            ApplicationUser user = userRepository.GetById(User.Identity.GetUserId());


            if (user.Medals.Count == 0) user.Medals.Add(new Medal() { Id = 7, Name = medalRepository.GetById(7).Name, ImageLink = medalRepository.GetById(7).ImageLink });
            if (user.Manuals.Count > 9 && user.Medals.FirstOrDefault(x => x.Id == 1) == null) user.Medals.Add(new Medal() { Id = 1, Name = medalRepository.GetById(1).Name, ImageLink = medalRepository.GetById(1).ImageLink });
            if (user.Manuals.Count > 29 && user.Medals.FirstOrDefault(x => x.Id == 2) == null) user.Medals.Add(new Medal() { Id = 2, Name = medalRepository.GetById(2).Name, ImageLink = medalRepository.GetById(2).ImageLink });

            if (user.Manuals.Sum(x => x.Comments.Count(y => y.UserId == user.Id)) > 14 && user.Medals.FirstOrDefault(x => x.Id == 3) == null) user.Medals.Add(new Medal() { Id = 3, Name = medalRepository.GetById(3).Name, ImageLink = medalRepository.GetById(3).ImageLink });
            Random i = new Random();
            if (i.Next(0, 50) == 10 && user.Medals.FirstOrDefault(x => x.Id == 4) == null) user.Medals.Add(new Medal() { Id = 4, Name = medalRepository.GetById(4).Name, ImageLink = medalRepository.GetById(4).ImageLink });

            if (user.Manuals.Count(x => x.Category.Name == "Math") > 0 && user.Medals.FirstOrDefault(x => x.Id == 5) == null) user.Medals.Add(new Medal() { Id = 5, Name = medalRepository.GetById(5).Name, ImageLink = medalRepository.GetById(5).ImageLink });

            if (user.Manuals.Count(x => x.Category.Name == "Literature") > 0 && user.Medals.FirstOrDefault(x => x.Id == 6) == null) user.Medals.Add(new Medal() { Id = 6, Name = medalRepository.GetById(6).Name, ImageLink = medalRepository.GetById(6).ImageLink });

            userRepository.Save();

        }

        [HttpPost]
        public ActionResult GetManualPage(string category, string tag, int page)
        {

            IEnumerable<ManualViewModel> manuals = new List<ManualViewModel>();
            int count=0;
            if (category != "" && tag != "") {  count= Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x => (x.Category.Name == category) && (x.Tags.FirstOrDefault(e => e.Name == tag) != null))).Count; }
            if (tag != "" && category == "") {  count = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x => x.Tags.FirstOrDefault(e => e.Name == tag) != null)).Count; }
            if (tag == "" && category != "") {  count = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x => x.Category.Name == category)).Count; }
            if (tag == "" && category == "") {  count = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll()).Count; }
            int position = count- ((page+1) * 7);
            int a = count - position;
            if (position<0) {a = 7 + position; position = 0; }
            if (a > 7) a = 7;
            if (a>0) { 
            if (category != "" && tag != "") { manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x =>( x.Category.Name == category)&&(x.Tags.FirstOrDefault(e=>e.Name== tag)!=null))).GetRange(position, a); }
            if (tag != ""&& category == "") { manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x => x.Tags.FirstOrDefault(e => e.Name == tag) != null)).GetRange(position, a); }
            if (tag == "" && category != "") { manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x => x.Category.Name == category)).GetRange(position, a); }
            if (tag == "" && category == "") { manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll()).GetRange(position, a); } }
            return Json(manuals, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTopManual(string UserId)
        {
            IEnumerable<ManualViewModel> manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x => (x.UserId == UserId) &&
            (x.Ratings.Count(r => r.Liked == true) - x.Ratings.Count(r => r.Liked == false)
            == manualRepository.GetAll().Where(z => z.UserId == UserId).Max(e => (e.Ratings.Count(r => r.Liked == true) - e.Ratings.Count(r => r.Liked == false))))));
            return Json(manuals, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetMostDiscussedManual(string UserId)
        {
            IEnumerable<ManualViewModel> manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll().Where(x=>(x.UserId==UserId)&&   (x.Comments.Count== manualRepository.GetAll().Where(z =>z.UserId == UserId).Max(y =>y.Comments.Count))));
            return Json(manuals, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult GetManuals()
        //{
        //    IEnumerable<ManualViewModel> manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll());
        //    return Json(manuals, JsonRequestBehavior.AllowGet);
        //}

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
                MedalsCheck();
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