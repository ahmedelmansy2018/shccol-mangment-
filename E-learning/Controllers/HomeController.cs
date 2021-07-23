using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning.Controllers
{
    public class HomeController : BaseController
    {//[AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        public string num()
        {
            int x = 5;
            int y = 0;
            int xx = x / y;
            return xx.ToString();
        }
      


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}