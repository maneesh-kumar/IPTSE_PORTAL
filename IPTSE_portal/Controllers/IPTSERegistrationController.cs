using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using IPTSE_portal.Models;
using System.IO;
using System.Security.Cryptography;
using System.Web.UI;

namespace IPTSE_portal.Controllers
{
    public class IPTSERegistrationController : Controller
    {
        private RegistrationDataModel db = new RegistrationDataModel();
        private LoginDataModel db1 = new LoginDataModel();
        // GET: IPTSERegistration
        //public ActionResult Index()
        //{
        //    return View(db.IPTSE_Reg_table.ToList());
        //}

        //// GET: IPTSERegistration/Details/5
        //public ActionResult Details(decimal id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    IPTSE_Reg_table iPTSE_Reg_table = db.IPTSE_Reg_table.Find(id);
        //    if (iPTSE_Reg_table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(iPTSE_Reg_table);
        //}

        // GET: IPTSERegistration/Create
        public ActionResult Registration()
        {
            return View();
        }

        // POST: IPTSERegistration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "Id,first_name,mid_name,last_name,dob,gender,email,fathername,mothername,country,addr1,addr2,city,state,zipcode,contact,password,confirmpassword,schoolname,standard,volunteername")] IPTSE_Reg_table_New iPTSE_Reg_table)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string clearText;

                    if (iPTSE_Reg_table.password != iPTSE_Reg_table.confirmpassword)
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Password and Confirm Password should be same!');</script>");
                    }
                    else
                    {
                        IPTSE_Reg_table iPTSE_Reg_table_New = new IPTSE_Reg_table();
                        iPTSE_Reg_table_New.Id = iPTSE_Reg_table.Id;
                        iPTSE_Reg_table_New.first_name = iPTSE_Reg_table.first_name;
                        iPTSE_Reg_table_New.mid_name = iPTSE_Reg_table.mid_name;
                        iPTSE_Reg_table_New.last_name = iPTSE_Reg_table.last_name;
                        iPTSE_Reg_table_New.dob = iPTSE_Reg_table.dob;
                        iPTSE_Reg_table_New.gender = iPTSE_Reg_table.gender;
                        iPTSE_Reg_table_New.email = iPTSE_Reg_table.email;
                        iPTSE_Reg_table_New.fathername = iPTSE_Reg_table.fathername;
                        iPTSE_Reg_table_New.mothername = iPTSE_Reg_table.mothername;
                        iPTSE_Reg_table_New.country = iPTSE_Reg_table.country;
                        iPTSE_Reg_table_New.addr1 = iPTSE_Reg_table.addr1;
                        iPTSE_Reg_table_New.addr2 = iPTSE_Reg_table.addr2;
                        iPTSE_Reg_table_New.city = iPTSE_Reg_table.city;
                        iPTSE_Reg_table_New.state = iPTSE_Reg_table.state;
                        iPTSE_Reg_table_New.zipcode = iPTSE_Reg_table.zipcode;
                        iPTSE_Reg_table_New.contact = iPTSE_Reg_table.contact;
                        iPTSE_Reg_table_New.schoolname = iPTSE_Reg_table.schoolname;
                        iPTSE_Reg_table_New.standard = iPTSE_Reg_table.standard;
                        iPTSE_Reg_table_New.volunteername = iPTSE_Reg_table.volunteername;
                        db.IPTSE_Reg_table.Add(iPTSE_Reg_table_New);
                        db.SaveChanges();
                        var lstId = db.IPTSE_Reg_table.OrderByDescending(t => t.Id).Select(t1 => t1.Id).FirstOrDefault();
                        login_table login_table = new login_table();
                        login_table.Id = lstId;
                        login_table.email = iPTSE_Reg_table.email;
                        login_table.password = iPTSE_Reg_table.password;
                        Createpassword(login_table);
                        //Session["id"] = iPTSE_Reg_table.Id;

                        string EncryptionKey = "MAKV2SPBNI99212";
                        byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(iPTSE_Reg_table.Id.ToString());
                        using (Aes encryptor = Aes.Create())
                        {
                            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                            encryptor.Key = pdb.GetBytes(32);
                            encryptor.IV = pdb.GetBytes(16);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                                {
                                    cs.Write(clearBytes, 0, clearBytes.Length);
                                    cs.Close();
                                }
                                clearText = Convert.ToBase64String(ms.ToArray());
                            }
                        }

                        string siteurl = "http://portal.iptse.com/IPTSELogin/Createpassword";
                        string smsg = "Dear User,<br/> Your Registration has been sucessfully done.";
                        smsg += "<br/>Please login with your email-id.<br/> Thank you <br/> IPTSE Admin.";
                        try
                        {
                            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                            message.To.Add(new MailAddress(iPTSE_Reg_table.email));
                            message.From = new MailAddress("admin@iptse.in");
                            message.Subject = "IPTSE Registration Confirmation Mail.";
                            message.Body = smsg;
                            message.IsBodyHtml = true;
                            SmtpClient client = new SmtpClient();
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.Port = 80;
                            client.Host = "smtpout.asia.secureserver.net";
                            NetworkCredential nc = new NetworkCredential("admin@iptse.in", "Admi@iptse5");
                            client.EnableSsl = false;
                            client.UseDefaultCredentials = true;
                            client.Credentials = nc;
                            client.Send(message);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMessage = "Unable to send mail...";
                            return View();
                        }
                        ViewData["success_msg"] = "Congratulation! you have Registered Successfully.";
                        return View("Successfull");
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                catch (Exception ex1)
                {
                    ViewBag.ErrorMessage = "Already Registered. if you are unable to login Please go through Forgot Password";
                    return View();
                }
            }
            return View(iPTSE_Reg_table);
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Createpassword(login_table login_table)
        {
            if (ModelState.IsValid)
            {
                //login_table.Id = Int32.Parse(Session["createpass"].ToString());
                try
                {
                    login_table.Id = login_table.Id;
                    login_table.email = login_table.email;
                    byte[] encode = new byte[login_table.password.Length];
                    encode = System.Text.Encoding.UTF8.GetBytes(login_table.password);
                    login_table.password = Convert.ToBase64String(encode);
                    login_table.Login_type = "Individual";
                    db1.login_table.Add(login_table);
                    db1.SaveChanges();
                    //TempData["Message"] = "Password Created Successfully. Login To Continue..";
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

        public ActionResult Successfull()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(IPTSE_Reg_table iPTSE_Reg_table)
        {
            var obj = db.IPTSE_Reg_table.Where(a => a.email.Equals(iPTSE_Reg_table.email)).FirstOrDefault();
            if (obj != null)
            {
                login_table login_Table = new login_table();
                var obj1 = db1.login_table.Where(a => a.Id.Equals(obj.Id)).FirstOrDefault();
                if (obj1 != null)
                {
                    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                    System.Text.Decoder strDecoder = encoder.GetDecoder();
                    byte[] to_DecodeByte = Convert.FromBase64String(obj1.password);
                    int charCount = strDecoder.GetCharCount(to_DecodeByte, 0, to_DecodeByte.Length);
                    char[] decoded_char = new char[charCount];
                    strDecoder.GetChars(to_DecodeByte, 0, to_DecodeByte.Length, decoded_char, 0);
                    string depass = new string(decoded_char);

                    string smsg = "Your User Id : " + obj.Id;
                    smsg += "\n Your Password : " + depass;


                    try
                    {
                        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                        message.To.Add(new MailAddress(iPTSE_Reg_table.email));
                        message.From = new MailAddress("admin@iptse.in");
                        message.Subject = "IPTSE Login Details";
                        message.Body = smsg;
                        //message.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient();
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Port = 80;
                        client.Host = "smtpout.asia.secureserver.net";
                        NetworkCredential nc = new NetworkCredential("admin@iptse.in", "Admi@iptse5");
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = true;
                        client.Credentials = nc;
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Unable to send mail..." + ex.Message;
                        return View();
                    }
                    ViewData["success_msg"] = "Your Id and password has been sent to your registered mail id!";
                    return View("Successfull");
                }
                else
                {
                    string clearText;
                    string EncryptionKey = "MAKV2SPBNI99212";
                    byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(obj.Id.ToString());
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(clearBytes, 0, clearBytes.Length);
                                cs.Close();
                            }
                            clearText = Convert.ToBase64String(ms.ToArray());
                        }
                    }

                    string siteurl = "http://portal.iptse.com/IPTSELogin/Login";
                    string smsg = "New Registration on our website.";
                    smsg += "Your account is activated now, please login with your email-id.";
                    try
                    {
                        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                        message.To.Add(new MailAddress(iPTSE_Reg_table.email));
                        message.From = new MailAddress("admin@iptse.in");
                        message.Subject = "Verification Mail";
                        message.Body = smsg;
                        message.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient();
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Port = 80;
                        client.Host = "smtpout.asia.secureserver.net";
                        NetworkCredential nc = new NetworkCredential("admin@iptse.in", "Admi@iptse5");
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = true;
                        client.Credentials = nc;
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Unable to send mail...";
                        return View();
                    }

                    ViewData["success_msg"] = "You did not yet create password!";
                    //ViewData["success_msg1"] = "Verification Mail has been sent to your registered Email-Id";
                    return View("Successfull");
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Email id is not Registered.";
                return View();
            }

        }

        //// GET: IPTSERegistration/Edit/5
        //public ActionResult Edit(decimal id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    IPTSE_Reg_table iPTSE_Reg_table = db.IPTSE_Reg_table.Find(id);
        //    if (iPTSE_Reg_table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(iPTSE_Reg_table);
        //}

        //// POST: IPTSERegistration/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,first_name,mid_name,last_name,gender,addr1,addr2,city,state,zipcode,country,contact,email,dob,schoolname,standard,mothername,fathername,volunteername")] IPTSE_Reg_table iPTSE_Reg_table)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(iPTSE_Reg_table).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(iPTSE_Reg_table);
        //}

        //// GET: IPTSERegistration/Delete/5
        //public ActionResult Delete(decimal id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    IPTSE_Reg_table iPTSE_Reg_table = db.IPTSE_Reg_table.Find(id);
        //    if (iPTSE_Reg_table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(iPTSE_Reg_table);
        //}

        //// POST: IPTSERegistration/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(decimal id)
        //{
        //    IPTSE_Reg_table iPTSE_Reg_table = db.IPTSE_Reg_table.Find(id);
        //    db.IPTSE_Reg_table.Remove(iPTSE_Reg_table);
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
