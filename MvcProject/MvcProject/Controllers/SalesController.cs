using MvcProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class SalesController : Controller
    {
        StockControlEntities context = new StockControlEntities();
        // GET: Sales
        public ActionResult Index()
        {
            var data = context.Sales.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult ProductBuy()
        {
            var productData = (from p in context.Products.ToList()
                               select new SelectListItem
                               {
                                   Text = p.Id.ToString(),
                                   Value = p.ProductName
                               });
            var customerData = (from c in context.Customers.ToList()
                                select new SelectListItem
                                {
                                    Text = c.Id.ToString(),
                                    Value = c.FirstName
                                });
            ViewBag.productData = productData;
            ViewBag.customerData = customerData;
            return View(); 
        }
        [HttpPost]
        public ActionResult ProductBuy(Sales sales)
        {
            var product = context.Products.Where(p=> p.Id==sales.ProductId).FirstOrDefault();
            var customer = context.Categories.Where(c => c.Id == sales.CustomerId).FirstOrDefault();
            sales.ProductId = product.Id;
            sales.CustomerId = customer.Id;
            var price = sales.NumberOfUnits * sales.Price;
            ViewBag.price = price;
            context.Sales.Add(sales);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SalesDelete(int id)
        {
            var deleted = context.Sales.Find(id);
            context.Sales.Remove(deleted);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}