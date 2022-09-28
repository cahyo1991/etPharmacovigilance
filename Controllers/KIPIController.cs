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
    public class KIPIController : Controller
    {
        // GET: KIPI
        public static string MainConnection = ConfigurationManager.AppSettings["MainConnection"];


        public List<ClinicalSymptom> ClinicalSymptom()
        {
            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "[TransactionOption]";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "GetSymptoms");
            conn.Open();
            List<ClinicalSymptom> Res = new List<ClinicalSymptom>();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ClinicalSymptom cs = new ClinicalSymptom();
                    cs.Id = dr["Id"].ToString();
                    cs.Name = dr["Name"].ToString();
                    Res.Add(cs);
                }
            }
            conn.Close();
            conn.Dispose();
            return Res;
        }

        public ActionResult Index()
        {
            ViewBag.ClinicalSymptom = ClinicalSymptom();
            ViewBag.javascript = "KIPI.js";
            return View();
        }
    }
}