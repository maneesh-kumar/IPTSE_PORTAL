using IPTSE_portal.BLL.Models;
using IPTSE_portal.Controllers.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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
                        client.BaseAddress = new Uri("http://localhost:63138/");
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
    }
}
