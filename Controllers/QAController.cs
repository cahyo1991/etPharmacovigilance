using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacovigilanceEtana.Controllers
{
    public class QAController : Controller
    {
        // GET: QA
        public ActionResult Index()
        {
            ViewBag.javascript = "QA.js";
            return View();
        }
    }
}