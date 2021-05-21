using BETApplicationMVC.Shopify.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BETApplicationMVC.Shopify.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult Landpage()
        {
            //string User = TempData["name"].ToString();
            //ViewBag.Name = User.ToString();
            return View();
        }

    }
}