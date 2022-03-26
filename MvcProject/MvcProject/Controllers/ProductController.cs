using MvcProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcProject.Controllers
{
    public class ProductController : Controller
    {
        StockControlEntities context = new StockControlEntities();
        // GET: Product
        public ActionResult Index(int page=1)
        {
            var data = context.Products.ToList().ToPagedList(page,10);
            return View(data);
        }

        [HttpGet]
        public ActionResult ProductAdd()
        {
            List<SelectListItem> data = (from p in context.Categories.ToList()
                                         select new SelectListItem
                                         {
                                             Text = p.CategoryName,
                                             Value = p.Id.ToString()
                                         }).ToList();
            ViewBag.data = data;
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Products product)
        {
            var NewProduct = context.Categories.Where(p => p.Id == product.Categories.Id).FirstOrDefault();
            product.Categories = NewProduct;
            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index"); //indexe git
        }

        public ActionResult ProductDelete(int id)
        {
            var deleted = context.Products.Find(id);
            context.Products.Remove(deleted);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ProductUpdate(int id)
        {
            var getProduct = context.Products.Find(id);
            List<SelectListItem> data = (from p in context.Categories.ToList()
                                         select new SelectListItem
                                         {
                                             Text = p.CategoryName,
                                             Value = p.Id.ToString()
                                         }).ToList();
            ViewBag.data = data;
            return View("ProductUpdate", getProduct);
        }

        [HttpPost]
        public ActionResult ProductUpdate(Products product)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var ctgrProduct = context.Products.Where(p => p.CategoryId == product.CategoryId).FirstOrDefault();
            var NewProduct = context.Products.Find(product.Id);
            NewProduct.ProductName = product.ProductName;
            NewProduct.Brand = product.Brand;
            NewProduct.CategoryId = ctgrProduct.CategoryId;
            NewProduct.Price = product.Price;
            NewProduct.UnitsInStock = product.UnitsInStock;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}