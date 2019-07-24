using PRP321MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace PRP321MVC.Controllers
{
    public class AccountController : Controller
    {

        [HttpPost]
        
        public ActionResult Login(FormCollection form)
        {
            LecturerModel lecturer = new LecturerModel(form["Username"].ToString(), form["Password"].ToString());
            List<AccountModel> accounts = lecturer.GetAccount(null, new List<string> { "username = '" + lecturer.Username + "'", "AccPassword = '" + lecturer.Password + "'" });

            if (accounts.Count > 0)
            {
                List<LecturerModel> lecList = lecturer.GetLecturer(null, new List<string> { "AccountID = " + accounts[0].AccId });
                lecturer = new LecturerModel( accounts[0].Username, accounts[0].Password, accounts[0].Priority, accounts[0].IsAdmin);
                lecturer.AccId = accounts[0].AccId;
                lecturer.Name = lecList[0].Name;
                lecturer.Surname = lecList[0].Surname;
                lecturer.Cell = lecList[0].Cell;
                lecturer.Email = lecList[0].Email;
                lecturer.Level = lecList[0].Level;
                lecturer.LectID = lecList[0].LectID;
                System.Web.HttpContext.Current.Session["USER"] = lecturer;
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                lecturer = null; //Invalid Credentials, Reset Lecturer Object
                return View("~/Views/Account/Login.cshtml", lecturer);
            }

            
        }

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword(int accid, FormCollection form)
        {
            LecturerModel lecturer = new LecturerModel();
            List<AccountModel> accounts = lecturer.GetAccount(null, new List<string> { "id = " + accid + "" });
            HttpCookie gdsjhgfjhsdgjhf = new HttpCookie("gdsjhgfjhsdgjhf", accid.ToString());
            Response.Cookies.Add(gdsjhgfjhsdgjhf);

            if (form["password"].ToString() != null)
            {
                lecturer.Password = form["password"].ToString();
                lecturer.ChangeAccount(new List<string> { "id = " + accid + "" });
            }

            return View();
        }
        [HttpGet]
        public ActionResult ForgotPasswordEmail()
        {

            return View();
        }

        public ActionResult ForgotPasswordEmail(FormCollection form)
        {
            try
            {
                LecturerModel lecturer = new LecturerModel(form["Username"].ToString(), "");
                List<AccountModel> accounts = lecturer.GetAccount(null, new List<string> { "username = '" + lecturer.Username + "'" });
                List<LecturerModel> lecList = lecturer.GetLecturer(null, new List<string> { "AccountID = " + accounts[0].AccId });

                if (lecList != null && accounts != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("seuzrulz@gmail.com");
                    mail.To.Add(lecList[0].Email);
                    mail.Subject = "Forgot Password";
                    mail.Body = "to reset your password please go to this location \n" +
                        "http://localhost:53901/Account/ForgotPassword?" +accounts[0].AccId;

                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential("seuzrulz@gmail.com", "psw");
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(mail);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["USER"] = null;
            return View("~/Views/Home/Index.cshtml");
        }
    }
}