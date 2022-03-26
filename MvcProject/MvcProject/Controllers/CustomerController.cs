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
    public class CustomerController : Controller
    {
        StockControlEntities context = new StockControlEntities();
        // GET: Customer
        public ActionResult Index(int page = 1)
        {

            var data = context.Customers.ToList().ToPagedList(page, 10);
            return View(data);
        }

      
        [HttpGet]
        public ActionResult CustomerAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerAdd(Customers customer)
        {
            if (!ModelState.IsValid)
            {
                return View("CustomerAdd");
            }
            context.Customers.Add(customer);
            context.SaveChanges();
            return View();
        }

        public ActionResult CustomerDelete(int id)
        {
            var deleted = context.Customers.Find(id);
            context.Customers.Remove(deleted);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CustomerUpdate(int id)
        {
            var getCustomer = context.Customers.Find(id);
            return View("CustomerUpdate", getCustomer);
        }

        [HttpPost]
        public ActionResult CustomerUpdate(Customers customer)
        {
            var getCustomer = context.Customers.Find(customer.Id);
            getCustomer.FirstName = customer.FirstName;
            getCustomer.LastName = customer.LastName;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}