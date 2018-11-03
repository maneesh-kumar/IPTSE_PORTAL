using IPTSE_portal.BLL.Models;
using IPTSE_portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Data.Entity.Validation;

namespace IPTSE_portal.Controllers
{
    public class SchoolRegistrationController : Controller
    {
        private RegistrationDataModel db = new RegistrationDataModel();
        private LoginDataModel db1 = new LoginDataModel();
        
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
                        client.BaseAddress = new Uri("http://portal.iptse.com/"); //localhost:63138/
                        //client.BaseAddress = new Uri("http://localhost:63138/"); 
                        var postTask = client.PostAsJsonAsync<SchoolRegistrationModel>("api/SchoolRegistrationAPI", schoolModel);
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewData["success_msg"] = "Congratulation! you have Registered Successfully.";
                            return View("Successfull");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Registration failed. Please contact IPTSE help line.";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BulkRegistration(HttpPostedFileBase postedFile)
        {
            HttpPostedFileBase file = Request.Files["postedFile"];
            List<IPTSE_Reg_table> student_Details = new List<IPTSE_Reg_table>();
            string filePath = string.Empty;
            
            if (postedFile != null)
            {
                try
                {
                    string path = Server.MapPath("~/App_Data/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (extension.Equals(".csv", StringComparison.InvariantCultureIgnoreCase))
                    {
                        postedFile.SaveAs(filePath);

                        //Read the contents of CSV file.
                        string csvData = System.IO.File.ReadAllText(filePath);

                        //Execute a loop over the rows.
                        char[] trimChars = { '\r', '\n' };
                        csvData = csvData.TrimEnd(trimChars);
                        string[] rows = csvData.Split('\n');
                        for (int i = 1; i < rows.Length; i++)
                        {
                            var rowparts = rows[i].Split(',');
                            IPTSE_Reg_table datarow = new IPTSE_Reg_table();

                            datarow.first_name = rowparts[0];
                            datarow.mid_name = rowparts[1];
                            datarow.last_name = rowparts[2];
                            datarow.dob = Convert.ToDateTime(rowparts[3]);
                            datarow.gender = rowparts[4];
                            datarow.email = rowparts[5];
                            datarow.fathername = rowparts[6];
                            datarow.mothername = rowparts[7];
                            datarow.country = rowparts[8];
                            datarow.addr1 = rowparts[9];
                            datarow.addr2 = rowparts[10];
                            datarow.city = rowparts[11];
                            datarow.state = rowparts[12];
                            datarow.zipcode = rowparts[13];
                            datarow.contact = rowparts[14];
                            datarow.schoolname = rowparts[15];
                            datarow.standard = rowparts[16];
                            datarow.volunteername = rowparts[17];
                            datarow.InstitutionType = string.Empty;
                            datarow.School_ID = Session["id"] != null ? Session["id"].ToString(): null;
                            student_Details.Add(datarow);
                            ViewBag.Message = "File uploaded successfully";
                            

                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please upload a csv file only";
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please verify, if you have selected a valid file";
            }

            ViewData["StudentDetails"] = student_Details;
            Session["StudentDetail"] = student_Details;
            return View("BulkReg");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Process()
        {
            ViewBag.Message = string.Empty;
            if (Session["StudentDetail"] != null)
            {
                try
                {
                    List<IPTSE_Reg_table> student_Details = new List<IPTSE_Reg_table>();
                    student_Details = Session["StudentDetail"] as List<IPTSE_Reg_table>;
                    foreach (IPTSE_Reg_table item in student_Details)
                    {
                        if (db.IPTSE_Reg_table.Count(c => c.email == item.email) > 0)
                        {
                            ViewBag.Message += "Email-id " + item.email + " already registred. Registration skiiped for this email-id. </br>";
                        }
                        else
                        {
                            db.IPTSE_Reg_table.Add(item);
                            db.SaveChanges();
                            var lstId = db.IPTSE_Reg_table.OrderByDescending(t => t.Id).Select(t1 => t1.Id).FirstOrDefault();
                            login_table login_table = new login_table();
                            login_table.Id = lstId;
                            login_table.email = item.email;
                            login_table.Login_type = "Individual";
                            var password = item.first_name + item.dob.ToString("_MMyyyy");
                            login_table.password = password;
                            var message = Createpassword(login_table);
                            if (message == "Success")
                            {
                                sendMail(login_table, item.first_name, password);
                            }
                            else
                            {
                                ViewBag.Message = message;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        ViewBag.Message = "All Registration are completed successfully. Please proceed for Payment.";
                    }
                }
                catch (DbEntityValidationException entityException)
                {
                    var validationerror = entityException.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault();
                    ViewBag.Message = validationerror.PropertyName +" " + validationerror.ErrorMessage;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.InnerException.Message;
                }
            }
            return View("BulkReg");
        }

        private void sendMail(login_table item, string firstName, string password)
        {
            string smsg = string.Format("Dear {0},<br/> Your Registration has been sucessfully done.", firstName);
            smsg += string.Format("Your User ID : {0},<br/>Your Password: {1}", item.Id, password);
            smsg += "<br/>Please login with your email-id or UserId.<br/> Thank you <br/> IPTSE Admin.";
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(new MailAddress(item.email));
                message.From = new MailAddress("info@iptse.in");
                message.Subject = "IPTSE Registration Confirmation Mail.";
                message.Body = smsg;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = 80;
                client.Host = "smtpout.asia.secureserver.net";
                NetworkCredential nc = new NetworkCredential("info@iptse.in", "Iptse@2018");
                client.EnableSsl = false;
                client.UseDefaultCredentials = true;
                client.Credentials = nc;
                client.Send(message);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Unable to send mail. Please contact support team for regisration confirmation";
            }
        }

        public string Createpassword(login_table login_table)
        {
            string returMsg = string.Empty;
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
                    returMsg = "Success";
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Contact Support team. Registration process failed for userid - " + login_table.Id;
                    returMsg = ViewBag.ErrorMessage;
                    return returMsg;
                }
            }
            else
                returMsg = "Contact Support team. Login Creation process failed for userid - " + login_table.Id; ;

            return returMsg;
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

        public ActionResult BulkReg()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "IPTSELogin");
            }
            return View();
        }
    }
}
