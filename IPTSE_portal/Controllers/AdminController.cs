using IPTSE_portal.BLL.Models;
using IPTSE_portal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IPTSE_portal.Controllers
{

    public class AdminController : Controller
    {
        private RegistrationDataModel db = new RegistrationDataModel();
        private PaymentDataModel paymentDb = new PaymentDataModel();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["admin_login"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            return View();
        }

        public ActionResult RegistrationList()
        {
            if (Session["admin_login"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            ViewData["RegTable"] = db.IPTSE_Reg_table.ToList();
            ViewData["PaymentTable"] = paymentDb.payment_details.ToList();
            return View();
        }

        public async Task<ActionResult> SchoolRegistrationList()
        {
            if (Session["admin_login"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://portal.iptse.com/"); //localhost:63138/
                    //client.BaseAddress = new Uri("http://localhost:63138/");
                    var response = await client.GetAsync("api/SchoolRegistrationAPI");
                    //postTask.Wait();
                    //var result = postTask.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        ViewData["SchoolList"] = JsonConvert.DeserializeObject<List<SchoolRegistrationModel>>(data);
                    }
                }
                return View();
            }
            catch (Exception ex1)
            {
                ViewBag.ErrorMessage = "Already Registered with this Email.";
                return View();
            }
        }

        public ActionResult Details(decimal id)
        {
            if (Session["admin_login"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (id > 0)
            {
                Session["DetailId"] = id;
            }

            IPTSE_Reg_table iPTSE_Reg_table = db.IPTSE_Reg_table.Find(id);
            payment_details paymentDetail = new payment_details();
            ViewData["RegInfo"] = iPTSE_Reg_table;
            ViewData["PaymentDetail"] = paymentDetail;
            if (iPTSE_Reg_table == null)
            {
                return HttpNotFound();
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Activate(payment_details paymentdetails)
        {
            if (Session["admin_login"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }

            try
            {
                ViewData["success_msg"] = "";
                if (ModelState.IsValid && paymentdetails != null)
                {
                    paymentDb.payment_details.Add(paymentdetails);
                    paymentDb.SaveChanges();
                    ViewData["success_msg"] = "User Successfully Activated";
                }
                else
                {
                    ViewBag.ErrorMessage = "Fill all valid payment details to activate the user";
                }
                return View("Successfull");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Some Error has occured. Please contact system administrator! </br>" + ex.InnerException.Message;
                return View();
            }

        }
    }
}