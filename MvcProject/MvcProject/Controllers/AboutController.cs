using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class AboutController : Controller
    {
        // GET: Abaout
        public ActionResult Index()
        {
            return View();
        }
    }
}