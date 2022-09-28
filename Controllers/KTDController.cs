using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacovigilanceEtana.Controllers
{
    public class KTDController : Controller
    {
        // GET: KTD
        public ActionResult Index()
        {
            ViewBag.javascript = "KTD.js";
            return View();
        }
    }
}