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
        public ActionResult Registration([Bind(Include = "Id,first_name,mid_name,last_name,gender,addr1,addr2,city,state,zipcode,country,contact,email,dob,schoolname,standerd,mothername,fathername,volunteername")] IPTSE_Reg_table iPTSE_Reg_table)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    string clearText;

                    db.IPTSE_Reg_table.Add(iPTSE_Reg_table);
                    db.SaveChanges();
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
                    
                    string siteurl = "http://www.iptse.com/IPTSELogin/Createpassword";
                    string smsg = "New Registration on our website, find your details below:\n";
                    smsg += "Your account is not activated still, please activate it by clicking here: ";
                    smsg +=  "<body><a href='"+siteurl + "?username;" + clearText+"'>Click here</a></body>" ;
                    smsg += "\n Your User Id is: " + iPTSE_Reg_table.Id;
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
                }
                catch(Exception ex1)
                {
                    ViewBag.ErrorMessage = "Already Registered. if you are unable to login Please go through Forgot Password";
                    return View();
                }
                ViewData["success_msg"] = "Congratulation! you have Registered Successfully.";
                ViewData["success_msg1"] = "Verification Mail has been sent to your registered Email-Id";
                return View("Successfull");                
                
            }
            return View(iPTSE_Reg_table);
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
                if(obj1!=null)
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
                        message.Subject = "Verification Mail";
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
                    ViewData["success_msg1"] = "";
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

                    string siteurl = "http://www.iptse.com/IPTSELogin/Createpassword";
                    string smsg = "New Registration on our website, find your details below:\n";
                    smsg += "Your account is not activated still, please activate it by clicking here: ";
                    smsg += "<body><a href='" + siteurl + "?username;" + clearText + "'>Click here</a></body>";
                    smsg += "\n Your User Id is: " + obj.Id;
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
                    ViewData["success_msg1"] = "Verification Mail has been sent to your registered Email-Id";
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
        //public ActionResult Edit([Bind(Include = "Id,first_name,mid_name,last_name,gender,addr1,addr2,city,state,zipcode,country,contact,email,dob,schoolname,standerd,mothername,fathername,volunteername")] IPTSE_Reg_table iPTSE_Reg_table)
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
