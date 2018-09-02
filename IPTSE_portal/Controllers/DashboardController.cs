using IPTSE_portal.BLL.Models;
using IPTSE_portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace IPTSE_portal.Controllers
{
    public class DashboardController : Controller
    {
        PaymentDataModel db = new PaymentDataModel();
        RegistrationDataModel reg = new RegistrationDataModel();
        // GET: Index
        public ActionResult Index()
        {
            if(Session["id"]== null)
            {
                return RedirectToAction("Login","IPTSELogin");
            }
            return View();
        }
        public ActionResult content()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            payment_details payment_Details = new payment_details();
            payment_Details.Id = Decimal.Parse(Session["id"].ToString());
            var obj = db.payment_details.Where(a => a.Id.Equals(payment_Details.Id)).FirstOrDefault();
            if (obj == null)
            {

                TempData["Message"] ="Please Do the payment first!";
                return RedirectToAction("payment", "Dashboard");
            }
            return View();
            
        }
        public ActionResult mocktest()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            payment_details payment_Details = new payment_details();
            payment_Details.Id = Decimal.Parse(Session["id"].ToString());
            var obj = db.payment_details.Where(a => a.Id.Equals(payment_Details.Id)).FirstOrDefault();
            if (obj == null)
            {

                TempData["Message"] = "Please Do the payment first!";
                return RedirectToAction("payment", "Dashboard");
            }
            return View();
        }
        public ActionResult payment()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            using (db)
            {
                payment_details payment_Details = new payment_details();
                payment_Details.Id = Decimal.Parse(Session["id"].ToString());
                var obj = db.payment_details.Where(a => a.Id.Equals(payment_Details.Id)).FirstOrDefault();
                if (obj != null)
                {
                    ViewBag.paymentid = obj.payment_id;
                    ViewBag.datetime = obj.payment_date;
                    ViewBag.id = obj.Id;
                    ViewBag.msg = "Payment has already been done by you. Please find the details below.";
                    return View("paymentdetails");
                  
                }
            }
            return View();
        }

        public ActionResult paymentdetails()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            try
            {
                string url = HttpContext.Request.Url.AbsoluteUri;
                payment_details payment_Details = new payment_details();
                string[] ar = url.Split('?');
                payment_Details.Id = Decimal.Parse(Session["id"].ToString());
                payment_Details.payment_date = DateTime.Now;
                payment_Details.payment_id = ar[1];
                db.payment_details.Add(payment_Details);
                db.SaveChanges();
                ViewBag.paymentid = ar[1];
                ViewBag.datetime = DateTime.Now;
                ViewBag.id = Session["id"];
            }
            catch
            {

            }
            
            return View();
        }

        public ActionResult UserProfile()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }

            if (Session["Type"] != null && Session["Type"].ToString() == "Institution")
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://portal.iptse.com/"); //localhost:63138/
                    //client.BaseAddress = new Uri("http://localhost:63138/"); 
                    var postTask = client.GetAsync("api/SchoolRegistrationAPI/" +  Session["id"].ToString());
                    postTask.Wait();
                    var result = postTask.Result;
                    //TODO - fix this if condition
                    if (result.IsSuccessStatusCode)
                    {
                        //ViewData["success_msg"] = "Congratulation! you have Registered Successfully.";
                        return View(result);
                    }
                    else
                    {
                        return View();
                    }
                }
                //BLL.Models.SchoolRegistrationModel iPTSE_Reg_Table = new BLL.Models.SchoolRegistrationModel();
                //iPTSE_Reg_Table.Id = Int32.Parse(Session["id"].ToString());
                //var obj = reg.IPTSE_Reg_table.Where(a => a.Id.Equals(iPTSE_Reg_Table.Id)).FirstOrDefault();
                //obj.contact = obj.contact.ToString();
                //return View(obj);
            }
            else
            {
                IPTSE_Reg_table iPTSE_Reg_Table = new IPTSE_Reg_table();
                iPTSE_Reg_Table.Id = Int32.Parse(Session["id"].ToString());
                var obj = reg.IPTSE_Reg_table.Where(a => a.Id.Equals(iPTSE_Reg_Table.Id)).FirstOrDefault();
                obj.contact = obj.contact.ToString();
                return View(obj);
            }
        }

        public ActionResult Setting()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            return View();
        }

        public ActionResult logout()
        {
            Session.Clear();
           
            return RedirectToAction("Login", "IPTSELogin");
        }
    }
}