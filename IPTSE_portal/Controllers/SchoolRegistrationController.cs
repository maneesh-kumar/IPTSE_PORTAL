using IPTSE_portal.BLL.Models;
using IPTSE_portal.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IPTSE_portal.Controllers
{
    public class SchoolRegistrationController : Controller
    {
        // GET: SchoolRegistration
        //public ActionResult Index()

        //{
        //    return View();
        //}

        //// GET: SchoolRegistration/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult SchoolRegistration()
        {
            return View();
        }

        // POST: SchoolRegistration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SchoolRegistration(SchoolRegistrationModel schoolModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://portal.iptse.com/"); //localhost:63138/
                        //client.BaseAddress = new Uri("http://localhost:63138/"); 
                        var postTask = client.PostAsJsonAsync<SchoolRegistrationModel>("api/SchoolRegistrationAPI", schoolModel);
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewData["success_msg"] = "Congratulation! you have Registered Successfully.";
                            return View("Successfull");
                        }
                    }
                }
                catch (Exception ex1)
                {
                    ViewBag.ErrorMessage = "Already Registered with this Email.";
                    return View();
                }
            }
            return View(schoolModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BulkRegistration(HttpPostedFileBase FileUpload)
        {
            DataTable dt = new DataTable();


            if (FileUpload.ContentLength > 0)
            {

                string fileName = Path.GetFileName(FileUpload.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);


                try
                {
                    FileUpload.SaveAs(path);

                    //dt = ProcessCSV(path);


                    ViewData["Feedback"] = "File uploaded successfully"; // ProcessBulkCopy(dt);
                }
                catch (Exception ex)
                {

                    ViewData["Feedback"] = ex.Message;
                }
            }
            else
            {

                ViewData["Feedback"] = "Please select a file";
            }


            dt.Dispose();

            return View("BulkReg", ViewData["Feedback"]);
        }

        private static DataTable ProcessCSV(string fileName)
        {

            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;


            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");


            StreamReader sr = new StreamReader(fileName);


            line = sr.ReadLine();
            strArray = r.Split(line);


            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));



            while ((line = sr.ReadLine()) != null)
            {
                row = dt.NewRow();


                row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }


            sr.Dispose();


            return dt;


        }


        //private static String ProcessBulkCopy(DataTable dt)
        //{
        //    string Feedback = string.Empty;
        //    string connString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;


        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {

        //        using (var copy = new SqlBulkCopy(conn))
        //        {


        //            conn.Open();


        //            copy.DestinationTableName = "BulkImportDetails";
        //            copy.BatchSize = dt.Rows.Count;
        //            try
        //            {

        //                copy.WriteToServer(dt);
        //                Feedback = "Upload complete";
        //            }
        //            catch (Exception ex)
        //            {
        //                Feedback = ex.Message;
        //            }
        //        }
        //    }

        //    return Feedback;
        //}


        private HttpWebRequest GetRequest(string url, string httpMethod = "POST", bool allowAutoRedirect = true)
        {
            Uri uri = new Uri(url);
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";

            request.Timeout = Convert.ToInt32(new TimeSpan(0, 5, 0).TotalMilliseconds);
            request.Method = httpMethod;
            return request;
        }


        // GET: SchoolRegistration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SchoolRegistration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolRegistration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SchoolRegistration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BulkReg()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            return View();
        }
    }
}
