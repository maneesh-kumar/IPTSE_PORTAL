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

                    var obj = db.login_table.Where(a => a.Id.Equals(login_table.Id) && a.password.Equals(login_table.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["id"] = obj.Id.ToString();
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        var obj1 = db.login_table.Where(a => a.Id.Equals(login_table.Id) && a.password.Equals(login_table.password)).FirstOrDefault();
                        if (obj1 != null)
                        {
                            Session["id"] = obj.Id.ToString();
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            if (login_table.Id == Decimal.Parse("91620195"))
                            {
                                if (login_table.password == "SVBUU0VfQURNSU5fTE9HSU4=")
                                {
                                    Session["admin_login"] = "91620195";
                                    return RedirectToAction("Index", "Admin");
                                }
                            }
                        }
                        ViewBag.ErrorMessage = "Invalid Credencial........";
                        return View();
                    }
                }
            }
            return View(login_table);
        }

        // GET: IPTSELogin/Create
        public ActionResult Createpassword()
        {
            string url = HttpContext.Request.Url.AbsoluteUri;

            string[] ar = url.Split(';');
            string result;
            string EncryptionKey = "MAKV2SPBNI99212";
            ar[1] = ar[1].Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(ar[1]);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    result = System.Text.Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            Session["createpass"] = result.ToString();
            return View();
        }

        // POST: IPTSELogin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createpassword([Bind(Include = "Id,password")] login_table login_table)
        {
            if (ModelState.IsValid)
            {
                login_table.Id = Int32.Parse(Session["createpass"].ToString());
                try
                {
                    byte[] encode = new byte[login_table.password.Length];
                    encode = System.Text.Encoding.UTF8.GetBytes(login_table.password);
                    login_table.password = Convert.ToBase64String(encode);
                    db.login_table.Add(login_table);
                    db.SaveChanges();
                    TempData["Message"]  = "Password Created Successfully. Login To Continue..";
                    return RedirectToAction("Login", "IPTSELogin");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Already Created! Please go through Forgot Password!";
                    return View();
                }

            }
            
            return View();
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
