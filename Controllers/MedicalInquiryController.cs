using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacovigilanceEtana.Controllers
{
    public class MedicalInquiryController : Controller
    {
        // GET: MedicalInquiry
        public ActionResult Index()
        {
            ViewBag.javascript = "MedicalInquiry.js";
            return View();
        }
    }
}