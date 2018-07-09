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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrationList()
        {
            return View(db.IPTSE_Reg_table.ToList());
        }

        public ActionResult Details(decimal id)
        {
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