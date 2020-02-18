using Br.Com.Posi.API.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Br.Com.Posi.Avarc.Web.Controllers
{
    public class HomeController : Controller
    {
        private string appId = "179540646671865";
        private string secredId = "cbbf3bf5cb201267ffe9f5971f2fd223";

        public ActionResult Index()
        {
            ViewBag.UrlFb = FacebookManager.GetInstance().Login(appId, secredId);
            return View();
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