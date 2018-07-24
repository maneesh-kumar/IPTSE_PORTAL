using IPTSE_portal.BLL.Models;
using IPTSE_portal.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    SchoolRegistrationAPIController objSchoolRegistration = new SchoolRegistrationAPIController();
                    objSchoolRegistration.Post(schoolModel);
                }
                catch
                {
                    return View();
                }
            }
            return View(schoolModel);
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
