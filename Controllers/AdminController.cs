using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacovigilanceEtana.Models;


namespace PharmacovigilanceEtana.Controllers
{
    public class AdminController : Controller
    {
        public static string MainConnection = ConfigurationManager.AppSettings["MainConnection"];
        // GET: Admin
        public ActionResult Index()
        {
            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }

            return View();
        }


        public ActionResult DetailMI(String idx) {

            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }
            ViewBag.javascript = "DetailMI.js";
            return View();
        }


        public ActionResult FormMI() {

            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }
            ViewBag.ListStatus = ListStatus();
            ViewBag.javascript = "FormMI.js";
            return View();
        }

        public List<ShowDetailManifestation> ShowDetailManifestation(String Id) {
            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "ReportKIPI";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "ShowDetailManifestation");
            cmd.Parameters.Add("@Id", Id);
            List<ShowDetailManifestation> Res = new List<ShowDetailManifestation>();
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ShowDetailManifestation sdm = new ShowDetailManifestation();
                    sdm.Id = dr["Id"].ToString();
                    sdm.Title = dr["Title"].ToString();
                    sdm.DateSymptoms = dr["DateSymptoms"].ToString();
                    sdm.TimeSymptoms = dr["TimeSymptoms"].ToString();
                    sdm.DurationSymptoms = dr["DurationSymptoms"].ToString();
                    sdm.DayStmptoms = dr["DayStmptoms"].ToString();
                    sdm.IdFormKIPI = dr["IdFormKIPI"].ToString();
                    Res.Add(sdm);
                }
            }
            conn.Close();
            conn.Dispose();
            return Res;

        }



        public List<ListDetailInternalVaccine> ListDetailInternalVaccine(String Id) {

            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "ReportKIPI";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "ShowDetailInternalVaccine");
            cmd.Parameters.Add("@Id", Id);
            List<ListDetailInternalVaccine> Res = new List<ListDetailInternalVaccine>();
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ListDetailInternalVaccine ld = new ListDetailInternalVaccine();
                    ld.Id = dr["Id"].ToString();
                    ld.IdVaccine = dr["IdVaccine"].ToString();
                    ld.BatchNo = dr["BatchNo"].ToString();
                    ld.Manufactur = dr["Manufactur"].ToString();
                    ld.Date = dr["Date"].ToString();
                    ld.Route = dr["Route"].ToString();
                    ld.InjectionSite = dr["InjectionSite"].ToString();
                    ld.TotalDose = dr["TotalDose"].ToString();
                    ld.DrugCondition = dr["DrugCondition"].ToString();
                    ld.OtherDrugCondition = dr["OtherDrugCondition"].ToString();
                    ld.HealthcareFacility = dr["HealthcareFacility"].ToString();
                    ld.IdFormKIPI = dr["IdFormKIPI"].ToString();
                    ld.ProductName = dr["ProductName"].ToString();
                    ld.ConditionDrug = dr["ConditionDrug"].ToString();
                    ld.Time = dr["Time"].ToString();
                    
                    Res.Add(ld);
                }
            }
            conn.Close();
            conn.Dispose();
            return Res;


        }

        public List<ShowDetailTreatMent> ShowDetailTreatMent(String Id)
        {

            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "ReportKIPI";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "ShowDetailTreatMent");
            cmd.Parameters.Add("@Id", Id);
            List<ShowDetailTreatMent> Res = new List<ShowDetailTreatMent>();
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ShowDetailTreatMent sdt = new ShowDetailTreatMent();
                    sdt.Id = dr["Id"].ToString();
                    sdt.Name = dr["Name"].ToString();
                    sdt.DateTreatmentAction = dr["DateTreatmentAction"].ToString();
                    sdt.HospitalTreatmentAction = dr["HospitalTreatmentAction"].ToString();
                    sdt.IdFormKIPI = dr["IdFormKIPI"].ToString();
                    Res.Add(sdt);
                }
            }
            conn.Close();
            conn.Dispose();
            return Res;
        }

            public ActionResult DetailKIPI(String Idx) {
            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }
            ViewBag.javascript = "DetailKIPI.js";
            ViewBag.ListDetailInternalVaccine = ListDetailInternalVaccine(Idx);
            ViewBag.ShowDetailManifestation = ShowDetailManifestation(Idx);
            ViewBag.ShowDetailTreatMent = ShowDetailTreatMent(Idx);
            return View();
        }

        public ActionResult FormKIPI() {
            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }
            ViewBag.ListStatus = ListStatus();
            ViewBag.javascript = "FormKIPI.js";
            return View();

        }


        public List<DetailInteralProduct> DetailInteralProduct(String Id) {
            List<DetailInteralProduct> Res = new List<DetailInteralProduct>();
            String sql = "[ReportKTD]";
            SqlConnection conn = new SqlConnection(MainConnection);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "ShowDetailInternalProduct");
            cmd.Parameters.Add("@Id", Id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DetailInteralProduct dp = new DetailInteralProduct();
                    dp.ProductName = dr["ProductName"].ToString();
                    dp.CategoryName = dr["CategoryName"].ToString();
                    dp.Id = dr["Id"].ToString();
                    dp.IdFormKTD = dr["IdFormKTD"].ToString();
                    dp.IdProduct = dr["IdProduct"].ToString();
                    dp.BatchNo = dr["BatchNo"].ToString();
                    dp.Manufactur = dr["Manufactur"].ToString();
                    dp.ExpDate = dr["ExpDate"].ToString();
                    dp.StartDate = dr["StartDate"].ToString();
                    dp.Dossage = dr["Dossage"].ToString();
                    dp.DrugCondition = dr["DrugCondition"].ToString();
                    dp.ProductAdministered = dr["ProductAdministered"].ToString();
                    dp.RouteProductAdministered = dr["RouteProductAdministered"].ToString();
                    dp.ProductTaken = dr["ProductTaken"].ToString();
                    dp.ProductSideEffectReturn = dr["ProductSideEffectReturn"].ToString();
                    dp.ProductStopStillHappening = dr["ProductStopStillHappening"].ToString();
                    dp.IdProductCategory = dr["IdProductCategory"].ToString();
                    dp.TherapyDuration = dr["TherapyDuration"].ToString();
                    dp.OtherDrugCondition = dr["OtherDrugCondition"].ToString();
                    Res.Add(dp);
                }
            }
            conn.Close();
            conn.Dispose();
            return Res;


        }


        public List<DetailExteralProduct> DetailExteralProduct(String Id) {
            List<DetailExteralProduct> Res = new List<DetailExteralProduct>();
            String Sql = "ReportKTD";
            SqlConnection conn = new SqlConnection(MainConnection);
            SqlCommand cmd = new SqlCommand(Sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "ShowDetailExternalProduct");
            cmd.Parameters.Add("@Id", Id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DetailExteralProduct de = new DetailExteralProduct();
                    de.Id = dr["Id"].ToString();
                    de.IdFormKTD = dr["IdFormKTD"].ToString();
                    de.ProductName = dr["ProductName"].ToString();
                    de.Dossage = dr["Dossage"].ToString();
                    de.StartDate = dr["StartDate"].ToString();
                    de.TherapyDuration = dr["TherapyDuration"].ToString();
                    de.Indication = dr["Indication"].ToString();
                    de.Frequency = dr["Frequency"].ToString();
                    de.Route = dr["Route"].ToString();
                    Res.Add(de);
                }
            }
            conn.Close();
            conn.Dispose();
            return Res;



        }
        public ActionResult DetailKtd(String Idx="") {
            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }
            //ViewBag.DetailKtd = GetReportDetailKTD(Idx);
            ViewBag.DetailInteralProduct = DetailInteralProduct(Idx);
            ViewBag.DetailExteralProduct = DetailExteralProduct(Idx);
            ViewBag.javascript = "DetailKtd.js";
            return View();

        }

        public List<ListStatus> ListStatus() {

            SqlConnection conn = new SqlConnection(MainConnection);
            string sql = "Select * From Status ";
            List<ListStatus> Res = new List<ListStatus>();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ListStatus ls = new ListStatus();
                    ls.Name = dr["Name"].ToString();
                    ls.Id = dr["Id"].ToString();
                    Res.Add(ls);
                }
            }
            conn.Close();
            conn.Dispose();
            return Res;
        
        }

        public ActionResult FormKTD() {
            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }

            ViewBag.ListStatus = ListStatus();
            ViewBag.javascript = "FormKTD.js";
            return View();

        }


        public ActionResult DetailQA() {
            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }
            ViewBag.Title = Request["Idx"].ToString();
            ViewBag.javascript = "DetailQA.js";
            return View();
        }

        public ActionResult FormQA() {
            if (Request.Cookies["IsLogin"] == null)
            {
                return RedirectToAction("Index", "SecureAdmin");
            }
            ViewBag.javascript = "FormQA.js";
            return View();

        
        }


    }
}