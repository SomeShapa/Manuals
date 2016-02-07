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
    [Authorize]
    [Culture]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext authContext;
        private readonly ManualRepository manualRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly TagRepository tagRepository;

        public HomeController()
        {
            authContext = new ApplicationDbContext();
            manualRepository = new ManualRepository(new ApplicationDbContext());
            categoryRepository = new CategoryRepository(new ApplicationDbContext());
            tagRepository = new TagRepository(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetManuals()
        {
            IEnumerable<ManualViewModel> manuals = Mapper.Map<List<ManualViewModel>>(manualRepository.GetAll());
            return Json(manuals, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateNewManual(string ReturnUrl)
        {
            ManualViewModel model = new ManualViewModel();
            ViewBag.Categories = GetCategoriesAsSelectList();
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
                Manual manual = Mapper.Map<Manual>(model);
                manualRepository.Add(manual);
                manualRepository.Save();
                return Json(new { result = "Redirect", url = ReturnUrl });
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult GetCategories()
        {
            IEnumerable<CategoryViewModel> categories = Mapper.Map<List<CategoryViewModel>>(categoryRepository.GetAll());
            return Json(categories, JsonRequestBehavior.AllowGet);
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

        private SelectList GetCategoriesAsSelectList()
        {
            return new SelectList(categoryRepository.GetAll(), "Id", "Name");
        }
    }
}