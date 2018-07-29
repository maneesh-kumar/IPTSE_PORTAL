using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IPTSE_portal.Models;
using System.Security.Cryptography;
using System.IO;

namespace IPTSE_portal.Controllers
{
    public class IPTSELoginController : Controller
    {
        private LoginDataModel db = new LoginDataModel();

        //// GET: IPTSELogin
        //public ActionResult Index()
        //{
        //    return View(db.login_table.ToList());
        //}

        //// GET: IPTSELogin/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    login_table login_table = db.login_table.Find(id);
        //    if (login_table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(login_table);
        //}

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(login_table login_table)
        {
            if (ModelState.IsValid)
            {
                               
                using (db)
                {
                    byte[] encode = new byte[login_table.password.Length];
                    encode = System.Text.Encoding.UTF8.GetBytes(login_table.password);
                    login_table.password = Convert.ToBase64String(encode);
                    bool allDigits = login_table.email.All(char.IsDigit);
                    if(allDigits)
                    {
                        var loginId = Int32.Parse(login_table.email);
                        var obj = db.login_table.Where(a => a.Id.Equals(loginId) && a.password.Equals(login_table.password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["id"] = obj.Id.ToString();
                            Session["Type"] = obj.Login_type;
                            //obj.LastLoginDateTime = DateTime.Now;
                            //db.login_table.Attach(obj);
                            //db.Entry(login_table).State = EntityState.Modified;
                            //db.SaveChanges();
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {

                            if (loginId == Decimal.Parse("91620195"))
                            {
                                if (login_table.password == "YWJjQDEyMw==") //"SVBUU0VfQURNSU5fTE9HSU4 =")
                                {
                                    Session["admin_login"] = "91620195";
                                    //obj.LastLoginDateTime = DateTime.Now;
                                    //db.login_table.Attach(obj);
                                    //db.Entry(login_table).State = EntityState.Modified;
                                    //db.SaveChanges();
                                    return RedirectToAction("Index", "Admin");
                                }
                            }
                            ViewBag.ErrorMessage = "Invalid Credentials....";
                            return View();
                        }

                     }
                    else
                    {
                        var obj1 = db.login_table.Where(a => a.email.Equals(login_table.email) && a.password.Equals(login_table.password)).FirstOrDefault();
                        if (obj1 != null)
                        {
                            Session["id"] = obj1.Id.ToString();
                            Session["Type"] = obj1.Login_type;
                            obj1.LastLoginDateTime = DateTime.Now;
                            //db.login_table.Add(obj1);
                            //// db.Entry(login_table).State = EntityState.Modified;
                            ////db.Entry(login_table).Property(x => x.LastLoginDateTime).IsModified = true;
                            //db.Entry(obj1).State = System.Data.Entity.EntityState.Modified;
                            //db.SaveChanges();
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            if (login_table.Id == Decimal.Parse("91620195"))
                            {
                                if (login_table.password == "SVBUU0VfQURNSU5fTE9HSU4=")
                                {
                                    Session["admin_login"] = "91620195";
                                    //obj1.LastLoginDateTime = DateTime.Now;
                                    //db.login_table.Attach(obj1);
                                    //db.Entry(login_table).State = EntityState.Modified;
                                    //db.SaveChanges();
                                    return RedirectToAction("Index", "Admin");
                                }
                            }
                            ViewBag.ErrorMessage = "Invalid Credentials....";
                            return View();
                        }
                    }
                  
                   
                }
            }
            return View(login_table);
        }

        
        


        //// GET: IPTSELogin/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    login_table login_table = db.login_table.Find(id);
        //    if (login_table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(login_table);
        //}

        //// POST: IPTSELogin/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,password")] login_table login_table)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(login_table).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(login_table);
        //}

        //// GET: IPTSELogin/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    login_table login_table = db.login_table.Find(id);
        //    if (login_table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(login_table);
        //}

        //// POST: IPTSELogin/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    login_table login_table = db.login_table.Find(id);
        //    db.login_table.Remove(login_table);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
