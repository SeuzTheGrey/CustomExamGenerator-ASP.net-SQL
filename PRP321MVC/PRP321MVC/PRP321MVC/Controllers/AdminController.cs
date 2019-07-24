using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRP321MVC.Models;

namespace PRP321MVC.Controllers
{
    public class AdminController : Controller
    {
        LecturerModel lecturer = System.Web.HttpContext.Current.Session["USER"] as LecturerModel;
        // GET: Admin
        public ActionResult Admin()
        {
            if (Session["USER"] != null)
            {
                if (lecturer.IsAdmin == 1)
                {
                    return View();
                }

            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult SelectAllUsers()
        {

            if (Session["USER"] != null)
            {
                if (lecturer.IsAdmin == 1)
                {
                    if (lecturer.Priority >= 7)
                    {
                        LecturerModel lecturer = new LecturerModel();
                        AccountModel account = new AccountModel();
                        List<AccountModel> accounts = account.GetAccount(null, null);
                        List<LecturerModel> lecturerList = lecturer.GetLecturer(null, null);

                        foreach (LecturerModel item in lecturerList)
                        {
                            foreach (AccountModel item2 in accounts)
                            {
                                if (item.AccId == item2.AccId)
                                {
                                    item.Username = item2.Username;
                                    item.Password = item2.Password;
                                    item.Priority = item2.Priority;
                                    item.IsAdmin = item2.IsAdmin;
                                }
                            }
                        }

                        return View(lecturerList);
                    }
                }

            }

            return RedirectToAction("Index", "Home");


        }



        public ActionResult SearchForUser(FormCollection form)
        {

            if (Session["USER"] != null)
            {
                if (lecturer.IsAdmin == 1)
                {
                    if (lecturer.Priority >= 7)
                    {
                        List<string> SearchWhere = new List<string>() { "username Like '%" + form["SearchRequirements"].ToString() + "%'" };

                        LecturerModel lecturer = new LecturerModel();
                        AccountModel account = new AccountModel();
                        List<AccountModel> accounts = account.GetAccount(null, SearchWhere);
                        List<LecturerModel> lecturerList = lecturer.GetLecturer(null, null);
                        List<LecturerModel> foundLecturers = new List<LecturerModel>();

                        foreach (AccountModel item in accounts)
                        {
                            foreach (LecturerModel item2 in lecturerList)
                            {
                                if (item.AccId == item2.AccId)
                                {
                                    foundLecturers.Add(new LecturerModel(item2.LectID, item2.Name, item2.Surname, item2.Cell, item2.Email, item2.Level, item.AccId, item.Username, item.Password, item.Priority, item.IsAdmin));
                                }
                            }
                        }

                        return View("SearchForUser", foundLecturers);
                    }
                }

            }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult Register()
        {
            if (Session["USER"] != null)
            {
                if (lecturer.IsAdmin == 1)
                {
                    if (lecturer.Priority >= 10)
                    {
                        return View();
                    }
                }

            }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult ReturnToRegisterIndex(FormCollection form)
        {

            if (Session["USER"] != null)
            {
                if (lecturer.IsAdmin == 1)
                {
                    if (lecturer.Priority >= 10)
                    {
                        LecturerModel lecturer = new LecturerModel();

                        lecturer = new LecturerModel(form["FirstName"].ToString(), form["LastName"].ToString(), form["Phone"].ToString(), form["Email"].ToString(), int.Parse(form["ProfessionalLevel"].ToString()), form["Username"].ToString(), form["Password"].ToString(), int.Parse(form["Priority"].ToString()), int.Parse(form["Adminstrator"].ToString()));

                        if (lecturer.AddAccount())
                        {
                            AccountModel account = new AccountModel();
                            List<AccountModel> accountList = account.GetAccount(null, null);

                            account = accountList[accountList.Count - 1];
                            lecturer.AccId = account.AccId;

                            if (lecturer.AddLecturer())
                            {
                                Directory.CreateDirectory("E:\\Third Year Project\\PRP321MVC\\PRP321MVC\\Exams\\" + lecturer.Username);
                                return RedirectToAction("Admin");
                            }
                            else
                            {
                                return RedirectToAction("Register");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Register");
                        }
                    }
                }

            }

            return RedirectToAction("Index", "Home");



        }

        public ActionResult NewAnswer()
        {
            if (Session["USER"] != null)
            {
                if (lecturer.IsAdmin == 1)
                {
                    if (lecturer.Priority >= 5)
                    {
                        return View();
                    }
                }

            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult NewAnswer(AnswerModel amObj)
        {

            if (Session["USER"] != null)
            {
                if (lecturer.IsAdmin == 1)
                {
                    if (lecturer.Priority >= 5)
                    {
                        bool result = amObj.AddAnswer();


                        if (result)
                        {
                            ViewData["result"] = "Successfully Inserted";

                        }
                        else
                        {
                            ViewData["result"] = "Insert Failed";
                        }

                        ModelState.Clear();
                        return View();
                    }
                }

            }

            return RedirectToAction("Index", "Home");



        }
    }
}