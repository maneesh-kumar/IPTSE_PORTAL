using IPTSE_portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            return View();
        }

        public ActionResult RegistrationList()
        {
            ViewData["RegTable"] = db.IPTSE_Reg_table.ToList();
            ViewData["PaymentTable"] = paymentDb.payment_details.ToList();
            return View();
        }

        public ActionResult Details(decimal id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IPTSE_Reg_table iPTSE_Reg_table = db.IPTSE_Reg_table.Find(id);
            if (iPTSE_Reg_table == null)
            {
                return HttpNotFound();
            }
            return View(iPTSE_Reg_table);

        }
    }
}