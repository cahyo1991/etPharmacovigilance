using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PharmacovigilanceEtana.Models;
using PharmacovigilanceEtana.Services;

namespace PharmacovigilanceEtana.Controllers
{
    public class ApiPVEtanaController : Controller
    {
        public static string MainConnection = ConfigurationManager.AppSettings["MainConnection"];
        // GET: ApiPVEtana
        public ActionResult Index()
        {
            return View();
        }





        public bool ValidateADLogin(string Username, string Password)
        {
            bool Success = false;
            try
            {
                DirectoryEntry Entry = new DirectoryEntry("LDAP://" + "ETANABIOTECH", Username, Password);
                DirectorySearcher Searcher = new DirectorySearcher(Entry);
                Searcher.SearchScope = System.DirectoryServices.SearchScope.OneLevel;
                SearchResult Results = Searcher.FindOne();
                Success = (Results != null);
            }
            catch
            {
                Success = false;
            }
            return Success;

        }

        



        public ActionResult LoopInsertFormKIPITreatment(String obj)
        {
            Result rr = new Result();
            try
            {
                dynamic dynJson = JsonConvert.DeserializeObject(obj);
                foreach (var item in dynJson)
                {
                    InsertFormKIPITreatment(
                       item.KIPITName.ToString(),
item.KIPITDateTreatmentAction.ToString(),
item.KIPITHospitalTreatmentAction.ToString(),
item.KIPITIdFormKIPI.ToString()
                        );
                }

                rr.Status = 1;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        public void InsertFormKIPITreatment(
  String KIPITName = "",
String KIPITDateTreatmentAction = "",
String KIPITHospitalTreatmentAction = "",
String KIPITIdFormKIPI = ""
          )
        {

            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "TransactionForm";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "InsertFormKIPITreatment");
            cmd.Parameters.Add("@KIPITName", KIPITName);
            cmd.Parameters.Add("@KIPITDateTreatmentAction", KIPITDateTreatmentAction);
            cmd.Parameters.Add("@KIPITHospitalTreatmentAction", KIPITHospitalTreatmentAction);
            cmd.Parameters.Add("@KIPITIdFormKIPI", KIPITIdFormKIPI);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

        }


        public ActionResult ListInputMI() {

            Result rr = new Result();
            try
            {
                List<ListInputMI> Res = new List<ListInputMI>();
                String sql = "ReportMI";
                SqlConnection conn = new SqlConnection(MainConnection);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "ShowAll");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListInputMI lm = new ListInputMI();
                        lm.ProductName = dr["ProductName"].ToString();
                        lm.Question = dr["Question"].ToString();
                        lm.Name = dr["Name"].ToString();
                        lm.Approriate = dr["Approriate"].ToString();
                        lm.Telephone = dr["Telephone"].ToString();
                        lm.Email = dr["Email"].ToString();
                        lm.Country = dr["Country"].ToString();
                        lm.Id = dr["Id"].ToString();
                        lm.ReceivedBy = dr["ReceivedBy"].ToString();
                        lm.LetterNo = dr["LetterNo"].ToString();
                        lm.CreatedDate = dr["CreatedDate"].ToString();
                        lm.Status = dr["Status"].ToString();
                        Res.Add(lm);
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = Res;
                rr.Message = "Success";

            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        public ActionResult ListDetailMI(String Id)
        {

            Result rr = new Result();
            try
            {
                List<ListInputMI> Res = new List<ListInputMI>();
                String sql = "ReportMI";
                SqlConnection conn = new SqlConnection(MainConnection);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "ShowDetail");
                cmd.Parameters.Add("@Id", Id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListInputMI lm = new ListInputMI();
                        lm.ProductName = dr["ProductName"].ToString();
                        lm.Question = dr["Question"].ToString();
                        lm.Name = dr["Name"].ToString();
                        lm.Approriate = dr["Approriate"].ToString();
                        lm.Telephone = dr["Telephone"].ToString();
                        lm.Email = dr["Email"].ToString();
                        lm.Country = dr["Country"].ToString();
                        lm.Id = dr["Id"].ToString();
                        lm.ReceivedBy = dr["ReceivedBy"].ToString();
                        lm.LetterNo = dr["LetterNo"].ToString();
                        lm.CreatedDate = dr["CreatedDate"].ToString();
                        lm.Images = dr["Images"].ToString();
                        Res.Add(lm);
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = Res;
                rr.Message = "Success";

            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        public ActionResult InsertFormMedicalInquiry(
String MI_ProductName = ""
, String MI_Question = ""
, String MI_Name = ""
, String MI_Approriate = ""
, String MI_OtherApproriate = ""
, String MI_Telephone = ""
, String MI_Email = ""
, String MI_Country = ""
, String MI_Images = ""
            ) {

            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "[TransactionForm]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "InsertFormMedicalInquiry");
                cmd.Parameters.Add("@MI_ProductName", MI_ProductName);
                cmd.Parameters.Add("@MI_Question", MI_Question);
                cmd.Parameters.Add("@MI_Name", MI_Name);
                cmd.Parameters.Add("@MI_Approriate", MI_Approriate);
                cmd.Parameters.Add("@MI_OtherApproriate", MI_OtherApproriate);
                cmd.Parameters.Add("@MI_Telephone", MI_Telephone);
                cmd.Parameters.Add("@MI_Email", MI_Email);
                cmd.Parameters.Add("@MI_Country", MI_Country);
                cmd.Parameters.Add("@MI_Images", MI_Images);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;
                rr.Message = "Success";
                
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        public ActionResult LoopInsertFormKIPISysmptoms(String obj)
        {
            Result rr = new Result();
            try
            {
                dynamic dynJson = JsonConvert.DeserializeObject(obj);
                foreach (var item in dynJson)
                {
                    InsertFormKIPISysmptoms(
                       item.KIPISTitle.ToString()
, item.KIPISDateSymptoms.ToString()
, item.KIPISTimeSymptoms.ToString()
, item.KIPISDurationSymptoms.ToString()
, item.KIPISDayStmptoms.ToString()
, item.KIPISIdFormKIPI.ToString()
                        );
                }

                rr.Status = 1;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public void InsertFormKIPISysmptoms(
 String KIPISTitle = ""
, String KIPISDateSymptoms = ""
, String KIPISTimeSymptoms = ""
, String KIPISDurationSymptoms = ""
, String KIPISDayStmptoms = ""
, String KIPISIdFormKIPI = ""
         )
        {

            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "TransactionForm";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "InsertFormKIPISysmptoms");
            cmd.Parameters.Add("@KIPISTitle", KIPISTitle);
            cmd.Parameters.Add("@KIPISDateSymptoms", KIPISDateSymptoms);
            cmd.Parameters.Add("@KIPISTimeSymptoms", KIPISTimeSymptoms);
            cmd.Parameters.Add("@KIPISDurationSymptoms", KIPISDurationSymptoms);
            cmd.Parameters.Add("@KIPISDayStmptoms", KIPISDayStmptoms);
            cmd.Parameters.Add("@KIPISIdFormKIPI", KIPISIdFormKIPI);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

        }


        public ActionResult LoopInsertFormKIPIInternal(String obj)
        {
            Result rr = new Result();
            try
            {
                dynamic dynJson = JsonConvert.DeserializeObject(obj);
                foreach (var item in dynJson)
                {
                    InsertFormKIPIInternal(
                       item.KIPIIPIdVaccine.ToString(),
item.KIPIIPBatchNo.ToString(),
item.KIPIIPManufactur.ToString(),
item.KIPIIPDate.ToString(),
item.KIPIIPRoute.ToString(),
item.KIPIIPInjectionSite.ToString(),
item.KIPIIPTotalDose.ToString(),
item.KIPIIPDrugCondition.ToString(),
item.KIPIIPOtherDrugCondition.ToString(),
item.KIPIIPHealthcareFacility.ToString(),
item.KIPIIPIdFormKIPI.ToString(),
item.KIPIIPTime.ToString()
                        );
                }

                rr.Status = 1;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public void InsertFormKIPIInternal(
String KIPIIPIdVaccine = ""
, String KIPIIPBatchNo = ""
, String KIPIIPManufactur = ""
, String KIPIIPDate = ""
, String KIPIIPRoute = ""
, String KIPIIPInjectionSite = ""
, String KIPIIPTotalDose = ""
, String KIPIIPDrugCondition = ""
, String KIPIIPOtherDrugCondition = ""
, String KIPIIPHealthcareFacility = ""
, String KIPIIPIdFormKIPI = ""
, String KIPIIPTime = ""
        )
        {

            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "TransactionForm";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "InsertFormKIPIInternal");
            cmd.Parameters.Add("@KIPIIPIdVaccine", KIPIIPIdVaccine);
            cmd.Parameters.Add("@KIPIIPBatchNo", KIPIIPBatchNo);
            cmd.Parameters.Add("@KIPIIPManufactur", KIPIIPManufactur);
            cmd.Parameters.Add("@KIPIIPDate", KIPIIPDate);
            cmd.Parameters.Add("@KIPIIPRoute", KIPIIPRoute);
            cmd.Parameters.Add("@KIPIIPInjectionSite", KIPIIPInjectionSite);
            cmd.Parameters.Add("@KIPIIPTotalDose", KIPIIPTotalDose);
            cmd.Parameters.Add("@KIPIIPDrugCondition", KIPIIPDrugCondition);
            cmd.Parameters.Add("@KIPIIPOtherDrugCondition", KIPIIPOtherDrugCondition);
            cmd.Parameters.Add("@KIPIIPHealthcareFacility", KIPIIPHealthcareFacility);
            cmd.Parameters.Add("@KIPIIPIdFormKIPI", KIPIIPIdFormKIPI);
            cmd.Parameters.Add("@KIPIIPTime", KIPIIPTime);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

        }


        public ActionResult HistoryUpdateForm(
            string UFStatus = ""
, string UFIdForm = ""
, string UFTypeForm = ""
, string UFUpdatedBy = ""
            ) {

            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "[TransactionForm]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "UpdateStatusForm");
                cmd.Parameters.Add("@UFStatus", UFStatus);
                cmd.Parameters.Add("@UFIdForm", UFIdForm);
                cmd.Parameters.Add("@UFTypeForm", UFTypeForm);
                cmd.Parameters.Add("@UFUpdatedBy", UFUpdatedBy);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;
                rr.Message = "Success";

            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }




        public ActionResult GetAllFormKTD() {
            Result rr = new Result();
            try
            {
                List<ListReportKTD> Res = new List<ListReportKTD>();

                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "ReportKTD";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "ShowAll");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListReportKTD lrp = new ListReportKTD();
                        lrp.LetterNo = dr["LetterNo"].ToString();
                        lrp.CreatedDate = dr["CreatedDate"].ToString();
                        lrp.Creator = dr["Creator"].ToString();
                        lrp.InitialName = dr["InitialName"].ToString();
                        lrp.ProductInternal = dr["ProductInternal"].ToString();
                        lrp.ProductExternal = dr["ProductExternal"].ToString();
                        lrp.DescribeAdverseEvent = dr["DescribeAdverseEvent"].ToString();
                        lrp.ReceivedBy = dr["ReceivedBy"].ToString();
                        lrp.Id = dr["Id"].ToString();
                        lrp.Status = dr["Status"].ToString();
                        lrp.PhoneEmail = dr["PhoneEmail"].ToString();
                        lrp.Sex = dr["Sex"].ToString();
                        lrp.Age = dr["Age"].ToString();
                        lrp.SideEffectStart = dr["SideEffectStart"].ToString();
                        lrp.SideEffectStop = dr["SideEffectStop"].ToString();
                        lrp.WillingContacted = dr["WillingContacted"].ToString();
                        Res.Add(lrp);
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = Res;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }




        public ActionResult ListFormKIPI() {
            Result rr = new Result();
            try
            {
                List<ListFormKIPI> Res = new List<ListFormKIPI>();
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "ReportKIPI";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "ShowAll");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListFormKIPI lr = new ListFormKIPI();
                        lr.LetterNo = dr["LetterNo"].ToString();
                        lr.CreatedDate = dr["CreatedDate"].ToString();
                        lr.ReporterName = dr["ReporterName"].ToString();
                        lr.PasienName = dr["PasienName"].ToString();
                        lr.Vaccine = dr["Vaccine"].ToString();
                        lr.Sysmptoms = dr["Sysmptoms"].ToString();
                        lr.ReceivedBy = dr["ReceivedBy"].ToString();
                        lr.Status = dr["Status"].ToString();
                        lr.Parent = dr["Parent"].ToString();
                        lr.Age = dr["Age"].ToString();
                        lr.City = dr["City"].ToString();
                        lr.Sex = dr["Sex"].ToString();
                        lr.Phone = dr["Phone"].ToString();
                        lr.Email = dr["Email"].ToString();
                        lr.RepoterPhone = dr["RepoterPhone"].ToString();
                        lr.RepoterEmail = dr["RepoterEmail"].ToString();
                        lr.ReporterPostalCode = dr["ReporterPostalCode"].ToString();
                        lr.ImmunizationGiver = dr["ImmunizationGiver"].ToString();
                        lr.FinalConditionPatient = dr["FinalConditionPatient"].ToString();
                        lr.SymptomsImmunized = dr["SymptomsImmunized"].ToString();
                        lr.SymptomsNotImmunized = dr["SymptomsNotImmunized"].ToString();
                        lr.TreatmentKIPI = dr["TreatmentKIPI"].ToString();
                        lr.OtherHealthInformation = dr["OtherHealthInformation"].ToString();
                        lr.WillingContacted = dr["WillingContacted"].ToString();
                        lr.Id = dr["Id"].ToString();
                        Res.Add(lr);

                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Message = "Success";
                rr.Return = Res;
            }
            catch (Exception ex)
            {
                rr.Status = 0;
                rr.Message = ex.Message;
                rr.Return = null;
            }


            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        public ActionResult GetReportDetailKTD(String Id)
        {
            Result rr = new Result();
            try
            {
                List<DetailReportKTD> Res = new List<DetailReportKTD>();
                String sql = "[ReportKTD]";
                SqlConnection conn = new SqlConnection(MainConnection);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "ShowDetail");
                cmd.Parameters.Add("@Id", Id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DetailReportKTD dd = new DetailReportKTD();
                        dd.Name = dr["Name"].ToString();
                        dd.PhoneEmail = dr["PhoneEmail"].ToString();
                        dd.Address = dr["Address"].ToString();
                        dd.PostalCode = dr["PostalCode"].ToString();
                        dd.City = dr["City"].ToString();
                        dd.InitialName = dr["InitialName"].ToString();
                        dd.Sex = dr["Sex"].ToString();
                        dd.YearAge = dr["YearAge"].ToString();
                        dd.MonthAge = dr["MonthAge"].ToString();
                        dd.Profession = dr["Profession"].ToString();
                        dd.HeightBody = dr["HeightBody"].ToString();
                        dd.WeightBody = dr["WeightBody"].ToString();
                        dd.DescribeAdverseEvent = dr["DescribeAdverseEvent"].ToString();
                        dd.OtherReleventInformation = dr["OtherReleventInformation"].ToString();
                        dd.SideEffectStart = dr["SideEffectStart"].ToString();
                        dd.SideEffectStop = dr["SideEffectStop"].ToString();
                        dd.SideEffectBad = dr["SideEffectBad"].ToString();
                        dd.FeelingPatient = dr["FeelingPatient"].ToString();
                        dd.ReportBPOM = dr["ReportBPOM"].ToString();
                        dd.ReceivedBy = dr["ReceivedBy"].ToString();
                        dd.Images = dr["Images"].ToString();
                        dd.LetterNo = dr["LetterNo"].ToString();
                        dd.WillingContacted = dr["WillingContacted"].ToString();
                        Res.Add(dd);
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = Res;
                rr.Message = "Success";

            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }


            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public ActionResult GetProvinces(String term, String page)
        {

            Result rr = new Result();

            select2 result = new select2();
            List<result_select> Res = new List<result_select>();
            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = String.Format("SELECT * FROM [IndonesianTerritory].[dbo].[Districts] where Name like '%{0}%'", term);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    result_select rs = new result_select();
                    rs.id = dr["Id"].ToString();
                    rs.text = dr["Name"].ToString();
                    //rs.selected = code == dr["Code"].ToString() ? true : false;
                    Res.Add(rs);
                }


            }

            conn.Close();
            conn.Dispose();
            result_select_2 rns = new result_select_2();
            rns.more = true;
            result.results = Res;
            result.pagination = rns;

            rr.Return = Res;
            rr.Status = 1;
            rr.Message = "Success";


            return new JsonResult()
            {
                Data = result,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult LoopInsertKTDExternalProduct(String obj)
        {
            Result rr = new Result();
            try
            {
                dynamic dynJson = JsonConvert.DeserializeObject(obj);
                foreach (var item in dynJson)
                {
                    InsertKTDExternalProduct(
                        item.KTDEPIdFormKTD.ToString(),
item.KTDEPProductName.ToString(),
item.KTDEPDossage.ToString(),
item.KTDEPStartDate.ToString(),
item.KTDEPTherapyDuration.ToString(),
item.KTDEPIndication.ToString(),
item.KTDEPFrequency.ToString(),
item.KTDEPRoute.ToString()
                        );
                }

                rr.Status = 1;
                rr.Message = "Success";
                }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public ActionResult LoopInsertKTDInternalProduct(String obj) {

            Result rr = new Result();
            try
            {
                dynamic dynJson = JsonConvert.DeserializeObject(obj);
                foreach (var item in dynJson)
                {
                    InsertKTDInternalProduct(
                        item.IdFormKTD.ToString(),
item.IdProduct.ToString(),
item.BatchNo.ToString(),
item.Manufactur.ToString(),
item.ExpDate.ToString(),
item.StartDate.ToString(),
item.Dossage.ToString(),
item.DrugCondition.ToString(),
item.ProductAdministered.ToString(),
item.RouteProductAdministered.ToString(),
item.ProductTaken.ToString(),
item.ProductSideEffectReturn.ToString(),
item.ProductStopStillHappening.ToString(),
item.IdProductCategory.ToString(),
item.TherapyDuration.ToString(),
item.OtherDrugCondition.ToString()
                        );
                }
                rr.Status = 1;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 1;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public void InsertKTDInternalProduct(
            String IdFormKTD = "",
String IdProduct = "",
String BatchNo = "",
String Manufactur = "",
String ExpDate = "",
String StartDate = "",
String Dossage = "",
String DrugCondition = "",
String ProductAdministered = "",
String RouteProductAdministered = "",
String ProductTaken = "",
String ProductSideEffectReturn = "",
String ProductStopStillHappening = "",
String IdProductCategory = "",
String TherapyDuration = "",
String OtherDrugCondition = ""
            ) {

            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "TransactionForm";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "InsertFormKTDInternalProduct");
            cmd.Parameters.Add("@KTDIPIdFormKTD", IdFormKTD);
            cmd.Parameters.Add("@KTDIPIdProduct", IdProduct);
            cmd.Parameters.Add("@KTDIPBatchNo", BatchNo);
            cmd.Parameters.Add("@KTDIPManufactur", Manufactur);
            cmd.Parameters.Add("@KTDIPExpDate", ExpDate);
            cmd.Parameters.Add("@KTDIPStartDate", StartDate);
            cmd.Parameters.Add("@KTDIPDossage", Dossage);
            cmd.Parameters.Add("@KTDIPDrugCondition", DrugCondition);
            cmd.Parameters.Add("@KTDIPProductAdministered", ProductAdministered);
            cmd.Parameters.Add("@KTDIPRouteProductAdministered", RouteProductAdministered);
            cmd.Parameters.Add("@KTDIPProductTaken", ProductTaken);
            cmd.Parameters.Add("@KTDIPProductSideEffectReturn", ProductSideEffectReturn);
            cmd.Parameters.Add("@KTDIPProductStopStillHappening", ProductStopStillHappening);
            cmd.Parameters.Add("@KTDIPIdProductCategory", IdProductCategory);
            cmd.Parameters.Add("@KTDIPTherapyDuration", TherapyDuration);
            cmd.Parameters.Add("@KTDIPOtherDrugCondition", OtherDrugCondition);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

        }





            public void InsertKTDExternalProduct(
            String KTDEPIdFormKTD = ""
, String KTDEPProductName = ""
, String KTDEPDossage = ""
, String KTDEPStartDate = ""
, String KTDEPTherapyDuration = ""
, String KTDEPIndication = ""
, String KTDEPFrequency = ""
, String KTDEPRoute = ""
            ) {

            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = "TransactionForm";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Param", "InsertFormKTDExternalProduct");
            cmd.Parameters.Add("@KTDEPIdFormKTD", KTDEPIdFormKTD);
            cmd.Parameters.Add("@KTDEPProductName", KTDEPProductName);
            cmd.Parameters.Add("@KTDEPDossage", KTDEPDossage);
            cmd.Parameters.Add("@KTDEPStartDate", KTDEPStartDate);
            cmd.Parameters.Add("@KTDEPTherapyDuration", KTDEPTherapyDuration);
            cmd.Parameters.Add("@KTDEPIndication", KTDEPIndication);
            cmd.Parameters.Add("@KTDEPFrequency", KTDEPFrequency);
            cmd.Parameters.Add("@KTDEPRoute", KTDEPRoute);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

        }



        public ActionResult InsertFormKIPI(
                   string KIPIName = ""
      , string KIPIPostalCode = ""
      , string KIPIParent = ""
      , string KIPIAge = ""
      , string KIPIAddress = ""
      , string KIPISex = ""
      , string KIPIIdCity = ""
      , string KIPIWus = ""
      , string KIPIPhone = ""
      , string KIPIKU = ""
      , string KIPIEmail = ""
      , string KIPIReporterName = ""
      , string KIPIRepoterPhone = ""
      , string KIPIReporterAddress = ""
      , string KIPIRepoterEmail = ""
      , string KIPIIdReporterCity = ""
      , string KIPIReporterPostalCode = ""
      , string KIPIImmunizationGiver = ""
      , string KIPIFinalConditionPatient = ""
      , string KIPIFinalConditionPatientDieDate = ""
      , string KIPISymptomsImmunized = ""
      , string KIPISymptomsNotImmunized = ""
      , string KIPITreatmentKIPI = ""
      , string KIPIOtherHealthInformation = ""
      , string KIPIWillingContacted = ""
      , string KIPICreatedBy = ""
      , string KIPICreatedDate = ""
      , string KIPIReceivedBy = ""
      , string KIPIImages = ""
      , string KIPILetterNo = ""
            ) {
            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "TransactionForm";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "InsertFormKIPI");
                cmd.Parameters.Add("@KIPIName", KIPIName);
                cmd.Parameters.Add("@KIPIPostalCode", KIPIPostalCode);
                cmd.Parameters.Add("@KIPIParent", KIPIParent);
                cmd.Parameters.Add("@KIPIAge", KIPIAge);
                cmd.Parameters.Add("@KIPIAddress", KIPIAddress);
                cmd.Parameters.Add("@KIPISex", KIPISex);
                cmd.Parameters.Add("@KIPIIdCity", KIPIIdCity);
                cmd.Parameters.Add("@KIPIWus", KIPIWus);
                cmd.Parameters.Add("@KIPIPhone", KIPIPhone);
                cmd.Parameters.Add("@KIPIKU", KIPIKU);
                cmd.Parameters.Add("@KIPIEmail", KIPIEmail);
                cmd.Parameters.Add("@KIPIReporterName", KIPIReporterName);
                cmd.Parameters.Add("@KIPIRepoterPhone", KIPIRepoterPhone);
                cmd.Parameters.Add("@KIPIReporterAddress", KIPIReporterAddress);
                cmd.Parameters.Add("@KIPIRepoterEmail", KIPIRepoterEmail);
                cmd.Parameters.Add("@KIPIIdReporterCity", KIPIIdReporterCity);
                cmd.Parameters.Add("@KIPIReporterPostalCode", KIPIReporterPostalCode);
                cmd.Parameters.Add("@KIPIImmunizationGiver", KIPIImmunizationGiver);
                cmd.Parameters.Add("@KIPIFinalConditionPatient", KIPIFinalConditionPatient);
                cmd.Parameters.Add("@KIPIFinalConditionPatientDieDate", KIPIFinalConditionPatientDieDate);
                cmd.Parameters.Add("@KIPISymptomsImmunized", KIPISymptomsImmunized);
                cmd.Parameters.Add("@KIPISymptomsNotImmunized", KIPISymptomsNotImmunized);
                cmd.Parameters.Add("@KIPITreatmentKIPI", KIPITreatmentKIPI);
                cmd.Parameters.Add("@KIPIOtherHealthInformation", KIPIOtherHealthInformation);
                cmd.Parameters.Add("@KIPIWillingContacted", KIPIWillingContacted);
                cmd.Parameters.Add("@KIPICreatedBy", KIPICreatedBy);
                cmd.Parameters.Add("@KIPICreatedDate", KIPICreatedDate);
                cmd.Parameters.Add("@KIPIReceivedBy", KIPIReceivedBy);
                cmd.Parameters.Add("@KIPIImages", KIPIImages);
                cmd.Parameters.Add("@KIPILetterNo", KIPILetterNo);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        rr.Message = dr["IdFormKIPI"].ToString();
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;

            }
            catch (Exception ex)
            {

                rr.Return = null;
                rr.Status = 0;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult UpdateReceiverMI(String Code, String Id, String Receiver)
        {

            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "[ReportMI]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "UpdateReceiver");
                cmd.Parameters.Add("@Code", Code);
                cmd.Parameters.Add("@Id", Id);
                cmd.Parameters.Add("@Receiver", Receiver);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }




        public ActionResult InsertFormKTD(
            String Name = "",
String Address = "",
String IdProvince = "",
String PhoneEmail = "",
String PostalCode = "",
String InitialName = "",
String YearAge = "",
String MonthAge = "",
String HeightBody = "",
String WeightBody = "",
String Sex = "",
String Profession = "",
String DescribeAdverseEvent = "",
String OtherReleventInformation = "",
String SideEffectStart = "",
String SideEffectStop = "",
String FeelingPatient = "",
String ReportBPOM = "",
String SideEffectBad = "",
String Images = "",
String WillingContacted = ""
            ) {
            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "TransactionForm";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "InsertFormKTD");
                cmd.Parameters.Add("@KTDName", Name);
                cmd.Parameters.Add("@KTDAddress", Address);
                cmd.Parameters.Add("@KTDIdProvince", IdProvince);
                cmd.Parameters.Add("@KTDPhoneEmail", PhoneEmail);
                cmd.Parameters.Add("@KTDPostalCode", PostalCode);
                cmd.Parameters.Add("@KTDInitialName", InitialName);
                cmd.Parameters.Add("@KTDYearAge", YearAge);
                cmd.Parameters.Add("@KTDMonthAge", MonthAge);
                cmd.Parameters.Add("@KTDHeightBody", HeightBody);
                cmd.Parameters.Add("@KTDWeightBody", WeightBody);
                cmd.Parameters.Add("@KTDSex", Sex);
                cmd.Parameters.Add("@KTDProfession", Profession);
                cmd.Parameters.Add("@KTDDescribeAdverseEvent", DescribeAdverseEvent);
                cmd.Parameters.Add("@KTDOtherReleventInformation", OtherReleventInformation);
                cmd.Parameters.Add("@KTDSideEffectStart", SideEffectStart);
                cmd.Parameters.Add("@KTDSideEffectStop", SideEffectStop);
                cmd.Parameters.Add("@KTDFeelingPatient", FeelingPatient);
                cmd.Parameters.Add("@KTDReportBPOM", ReportBPOM);
                cmd.Parameters.Add("@KTDSideEffectBad", SideEffectBad);
                cmd.Parameters.Add("@KTDImages", Images);
                cmd.Parameters.Add("@KTDWillingContacted", WillingContacted);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        rr.Message = dr["IdFormKTD"].ToString();
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;

            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public ActionResult UploadImageKTD(HttpPostedFileBase[] Image = null
          )
        {
            Result rr = new Result();
            try
            {
                String fileName = "0";
                String FileNames = "";
                int i = 0;
                if (Image != null)
                {
                    
                    
                    foreach (HttpPostedFileBase file in Image)
                    {
                        i = i + 1;
                        if (file != null)
                        {
                            DateTime today = DateTime.Now;
                            String fileExtension = Path.GetExtension(file.FileName);
                            string dateTimeNow = Regex.Replace(DateTime.Now.ToString(), "[ :/]", string.Empty);
                            fileName = "upload_" + i.ToString() + dateTimeNow + fileExtension;
                            FileNames = FileNames + fileName + ";";
                            string filePath = this.Server.MapPath("~/Asset/ImageKTD/");
                            var res = uploadFile(file, filePath, fileName);

                        }

}

                    
                }
               

                rr.Return = null;
                rr.Message = FileNames;
                rr.Status = 1;

            }
            catch (Exception ex)
            {

                rr.Return = null;
                rr.Message = ex.Message;
                rr.Status = 0;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };


        }

        public ActionResult uploadFile(HttpPostedFileBase file, string baseFolderPath, string fileName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(baseFolderPath);
            if (!directoryInfo.Exists)
                directoryInfo.Create();
            try
            {
                file.SaveAs(directoryInfo + fileName);
                Dictionary<string, string> Message = new Dictionary<string, string>();
                Message["result"] = "Sukses!";

                return new JsonResult()
                {
                    Data = Message,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
            catch (Exception ex)
            {
                Dictionary<string, string> Message = new Dictionary<string, string>();
                Message["ImageName"] = "Sukses!";

                return new JsonResult()
                {
                    Data = ex.Message,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
            Dictionary<string, string> Messagesd = new Dictionary<string, string>();
            Messagesd["result"] = "Sukses!";

            return new JsonResult()
            {
                Data = Messagesd,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        public ActionResult GetSearchProduct(String term, String page)
        {

            Result rr = new Result();

            select2 result = new select2();
            List<result_select> Res = new List<result_select>();
            SqlConnection conn = new SqlConnection(MainConnection);
            String sql = String.Format("SELECT * FROM Products where Name like '%{0}%'", term);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    result_select rs = new result_select();
                    rs.id = dr["Id"].ToString();
                    rs.text = dr["Name"].ToString();
                    //rs.selected = code == dr["Code"].ToString() ? true : false;
                    Res.Add(rs);
                }


            }

            conn.Close();
            conn.Dispose();
            result_select_2 rns = new result_select_2();
            rns.more = true;
            result.results = Res;
            result.pagination = rns;

            rr.Return = Res;
            rr.Status = 1;
            rr.Message = "Success";


            return new JsonResult()
            {
                Data = result,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public ActionResult ListDetailKIPI(String Id) {
            Result rr = new Result();
            try
            {
                List<ListDetailKIPI> Res = new List<ListDetailKIPI>();

                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "ReportKIPI";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", Id);
                cmd.Parameters.Add("@Param", "ShowDetail");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListDetailKIPI ld = new ListDetailKIPI();
                        ld.Name = dr["Name"].ToString();
                        ld.PostalCode = dr["PostalCode"].ToString();
                        ld.Parent = dr["Parent"].ToString();
                        ld.Age = dr["Age"].ToString();
                        ld.Address = dr["Address"].ToString();
                        ld.Sex = dr["Sex"].ToString();
                        ld.City = dr["City"].ToString();
                        ld.Wus = dr["Wus"].ToString();
                        ld.Phone = dr["Phone"].ToString();
                        ld.KU = dr["KU"].ToString();
                        ld.Email = dr["Email"].ToString();
                        ld.ReporterName = dr["ReporterName"].ToString();
                        ld.RepoterPhone = dr["RepoterPhone"].ToString();
                        ld.ReporterAddress = dr["ReporterAddress"].ToString();
                        ld.RepoterEmail = dr["RepoterEmail"].ToString();
                        ld.ReporterCity = dr["ReporterCity"].ToString();
                        ld.ReporterPostalCode = dr["ReporterPostalCode"].ToString();
                        ld.ImmunizationGiver = dr["ImmunizationGiver"].ToString();
                        ld.FinalConditionPatient = dr["FinalConditionPatient"].ToString();
                        ld.FinalConditionPatientDieDate = dr["FinalConditionPatientDieDate"].ToString();
                        ld.SymptomsImmunized = dr["SymptomsImmunized"].ToString();
                        ld.SymptomsNotImmunized = dr["SymptomsNotImmunized"].ToString();
                        ld.TreatmentKIPI = dr["TreatmentKIPI"].ToString();
                        ld.OtherHealthInformation = dr["OtherHealthInformation"].ToString();
                        ld.WillingContacted = dr["WillingContacted"].ToString();
                        ld.ReceivedBy = dr["ReceivedBy"].ToString();
                        ld.Images = dr["Images"].ToString();
                        ld.LetterNo = dr["LetterNo"].ToString();
                        Res.Add(ld);
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = Res;
                rr.Message = "Success";
                
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        public ActionResult UpdateReceiverKTD(String Code, String Id, String Receiver)
        {

            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "[ReportKTD]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "UpdateReceiver");
                cmd.Parameters.Add("@Code", Code);
                cmd.Parameters.Add("@Id", Id);
                cmd.Parameters.Add("@Receiver", Receiver);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public ActionResult UpdateReceiverKIPI(String Code, String Id, String Receiver)
        {

            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "[ReportKIPI]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "UpdateReceiver");
                cmd.Parameters.Add("@Code", Code);
                cmd.Parameters.Add("@Id", Id);
                cmd.Parameters.Add("@Receiver", Receiver);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public ActionResult UpdateReceiver(String Param, String Code, String Id, String Receiver, String IdComplaintCriteria) {

            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "[ReportQA]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", Param);
                cmd.Parameters.Add("@Code", Code);
                cmd.Parameters.Add("@Id", Id);
                cmd.Parameters.Add("@Receiver", Receiver);
                cmd.Parameters.Add("@IdComplaintCriteria", IdComplaintCriteria);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult UpdateHeaderForm(String Id , String EffectiveDate , String NoDocument) {

            Result rr = new Result();
            try
            {
                
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "GetHeaderReport";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "Update");
                cmd.Parameters.Add("@Id", Id);
                cmd.Parameters.Add("@EffectiveDate", EffectiveDate);
                cmd.Parameters.Add("@NoDocument", NoDocument);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;
                rr.Message = "Success";

            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };



        }



        public ActionResult GetHeaderForm(String Form) {
            Result rr = new Result();
            try
            {
                List<HeaderReport> Res = new List<HeaderReport>();
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "GetHeaderReport";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "GetHeader");
                cmd.Parameters.Add("@Form", Form);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HeaderReport hr = new HeaderReport();
                        hr.EffectiveDate = dr["EffectiveDate"].ToString();
                        hr.NoDocument = dr["NoDocument"].ToString();
                        hr.Id = dr["Id"].ToString();
                        Res.Add(hr);
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = Res;
                rr.Message = "Success";

            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }




        public ActionResult GetFormQA(String Param , String Id = "") {
            Result rr = new Result();
            try
            {
                List<FormQA> Res = new List<FormQA>();
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "ReportQA";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", Param);
                cmd.Parameters.Add("@Id", Id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        FormQA fq = new FormQA();
                        fq.Id = dr["Id"].ToString();
                        fq.CreatedBy = dr["CreatedBy"].ToString();
                        fq.CreatedDate = dr["CreatedDate"].ToString();
                        fq.UpdatedBy = dr["UpdatedBy"].ToString();
                        fq.UpdatedDate = dr["UpdatedDate"].ToString();
                        fq.Status = dr["Status"].ToString();
                        fq.IdCustomerType = dr["IdCustomerType"].ToString();
                        fq.CustomerTypeOthers = dr["CustomerTypeOthers"].ToString();
                        fq.Address = dr["Address"].ToString();
                        fq.Telephone = dr["Telephone"].ToString();
                        fq.IdProduct = dr["IdProduct"].ToString();
                        fq.BatchNo = dr["BatchNo"].ToString();
                        fq.ExpDate = dr["ExpDate"].ToString();
                        fq.BuyDate = dr["BuyDate"].ToString();
                        fq.AmountComplained = dr["AmountComplained"].ToString();
                        fq.ExampleAmount = dr["ExampleAmount"].ToString();
                        fq.IdComplaintCategory = dr["IdComplaintCategory"].ToString();
                        fq.IdComplaintCriteria = dr["IdComplaintCriteria"].ToString();
                        fq.ProductCondition = dr["ProductCondition"].ToString();
                        fq.Description = dr["Description"].ToString();
                        fq.Receiver = dr["Receiver"].ToString();
                        fq.LetterNo = dr["LetterNo"].ToString();
                        fq.CustomerType = dr["CustomerType"].ToString();
                        fq.Product = dr["Product"].ToString();
                        fq.ComplaintCategory = dr["ComplaintCategory"].ToString();
                        fq.ComplaintCriteria = dr["ComplaintCriteria"].ToString();
                        fq.UsernameProduct = dr["UsernameProduct"].ToString();
                        Res.Add(fq);
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = Res;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }

            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }




        public ActionResult LoginAdmin(String Code, String Password)
        {
            Result rr = new Result();
            try
            {

                bool cekAd = ValidateADLogin(Code, Password);

                if (cekAd == false)
                {

             

                List<Users> res = new List<Users>();
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "Login";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Code", Code);
                cmd.Parameters.Add("@Password", Password);
                    cmd.Parameters.Add("@Param", "LoginNotAD");
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //Users us = new Users();
                        //us.Code

                        CookiesManager.createCookie(this, "Code", dr["ActiveDirectory"].ToString());
                        CookiesManager.createCookie(this, "Name", dr["FullName"].ToString());
                        CookiesManager.createCookie(this, "Role", dr["CodeDepartment"].ToString());
                        CookiesManager.createCookie(this, "IsLogin", "1");
                        rr.Status = 1;
                        rr.Return = null;
                        rr.Message = dr["CodeDepartment"].ToString();


                    }
                 
                }
                else
                {
                    rr.Status = 0;
                    rr.Message = "Kombinasi User & Password Salah !";
                    rr.Return = null;
                }

                }
                else
                {



                    List<Users> res = new List<Users>();
                    SqlConnection conn = new SqlConnection(MainConnection);
                    String sql = "Login";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Code", Code);
                    cmd.Parameters.Add("@Password", Password);
                    cmd.Parameters.Add("@Param", "LoginAD");
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //Users us = new Users();
                            //us.Code

                            CookiesManager.createCookie(this, "Code", dr["ActiveDirectory"].ToString());
                            CookiesManager.createCookie(this, "Name", dr["FullName"].ToString());
                            CookiesManager.createCookie(this, "Role", dr["CodeDepartment"].ToString());
                            CookiesManager.createCookie(this, "IsLogin", "1");
                            rr.Status = 1;
                            rr.Return = null;
                            rr.Message = dr["CodeDepartment"].ToString();


                        }

                    }
                    else
                    {
                        rr.Status = 0;
                        rr.Message = "Kombinasi User & Password Salah !";
                        rr.Return = null;
                    }


                }
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Message = ex.Message;
                rr.Return = null;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        public ActionResult Logout()
        {
            //Session.Clear();

            //FormsAuthentication.SignOut();
            CookiesManager.clearAllCookies(this);
            return RedirectToAction("Index", "SecureAdmin");

        }


            public ActionResult InsertQA(
        String QACreator = ""
      , String QAStatus = ""
      , String QAIdCustomerType = ""
      , String QACustomerTypeOthers = ""
      , String QAAddress = ""
      , String QATelephone = ""
      , String QAIdProduct = ""
      , String QABatchNo = ""
      , String QAExpDate = ""
      , String QABuyDate = ""
      , String QAAmountComplained = ""
      , String QAExampleAmount = ""
      , String QAIdComplaintCategory = ""
      , String QAIdComplaintCriteria = ""
      , String QAProductCondition = ""
      , String QADescription = ""
      , String QAReceiver = ""
      , String QALetterNo = ""
                , String QAUserNameProduct = ""
            ) {

            Result rr = new Result();
            try
            {
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "TransactionForm";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", "InsertFormQA");
                cmd.Parameters.Add("@QACreator", QACreator);
                cmd.Parameters.Add("@QAStatus", QAStatus);
                cmd.Parameters.Add("@QAIdCustomerType", QAIdCustomerType);
                cmd.Parameters.Add("@QACustomerTypeOthers", QACustomerTypeOthers);
                cmd.Parameters.Add("@QAAddress", QAAddress);
                cmd.Parameters.Add("@QATelephone", QATelephone);
                cmd.Parameters.Add("@QAIdProduct", QAIdProduct);
                cmd.Parameters.Add("@QABatchNo", QABatchNo);
                cmd.Parameters.Add("@QAExpDate", QAExpDate);
                cmd.Parameters.Add("@QABuyDate", QABuyDate);
                cmd.Parameters.Add("@QAAmountComplained", QAAmountComplained);
                cmd.Parameters.Add("@QAExampleAmount", QAExampleAmount);
                cmd.Parameters.Add("@QAIdComplaintCategory", QAIdComplaintCategory);
                cmd.Parameters.Add("@QAIdComplaintCriteria", QAIdComplaintCriteria);
                cmd.Parameters.Add("@QAProductCondition", QAProductCondition);
                cmd.Parameters.Add("@QADescription", QADescription);
                cmd.Parameters.Add("@QAReceiver", QAReceiver);
                cmd.Parameters.Add("@QALetterNo", QALetterNo);
                cmd.Parameters.Add("@QAUserNameProduct", QAUserNameProduct);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = null;
                rr.Message = "Success";


            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }



        public ActionResult GetOption(String Param) {
            Result rr = new Result();
            try
            {

                List<Option> Res = new List<Option>();
                SqlConnection conn = new SqlConnection(MainConnection);
                String sql = "TransactionOption";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Param", Param);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Option op = new Option();
                        op.Id = dr["Id"].ToString();
                        op.Name = dr["Name"].ToString();
                        Res.Add(op);
                    }
                }
                conn.Close();
                conn.Dispose();
                rr.Status = 1;
                rr.Return = Res;
                rr.Message = "Success";
            }
            catch (Exception ex)
            {

                rr.Status = 0;
                rr.Return = null;
                rr.Message = ex.Message;
            }
            return new JsonResult()
            {
                Data = rr,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


    }
}