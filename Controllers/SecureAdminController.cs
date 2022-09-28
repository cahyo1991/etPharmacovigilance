using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacovigilanceEtana.Controllers
{
    public class SecureAdminController : Controller
    {
        // GET: SecureAdmin
        public ActionResult Index()
        {
            if (Request.Cookies["IsLogin"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.javascript = "SecureAdmin.js";
            return View();
        }
    }
}