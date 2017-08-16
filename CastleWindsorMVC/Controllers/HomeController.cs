using Castle.Core.Logging;
using CastleWindsorMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CastleWindsorMVC.Controllers
{
    public class HomeController : Controller
    {
        public ILogger Logger { get; set; }
        public IRepository<object> Repo { get; set;}

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            Logger.Warn($"Home Page Init");

            return View();
        }
    }
}
