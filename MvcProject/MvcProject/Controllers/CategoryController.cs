using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcProject.Controllers
{
    public class CategoryController : Controller
    {
        StockControlEntities Context = new StockControlEntities();
        // GET: Category
        public ActionResult Index(int page=1)
        {
            var data = Context.Categories.ToList().ToPagedList(page,4);
            return View(data);
        }
        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Categories category)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoryAdd");
            }
            Context.Categories.Add(category);
            Context.SaveChanges();
            return View();
        }

      
        public ActionResult CategoryDelete(int id)
        {
            var deleted = Context.Categories.Find(id);
            Context.Categories.Remove(deleted);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CategoryUpdate(int id)
        {
            var getCategory = Context.Categories.Find(id);
            return View("CategoryUpdate", getCategory);
        }

        [HttpPost]
        public ActionResult CategoryUpdate(Categories category)
        {
            var getCategory = Context.Categories.Find(category.Id);
            getCategory.CategoryName = category.CategoryName;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update()
        {
            return View();
        }
    }
}