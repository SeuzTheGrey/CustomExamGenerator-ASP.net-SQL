using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRP321MVC.Models;

namespace PRP321MVC.Controllers
{
    public class ProfileController : Controller
    {
        LecturerModel lecturer = System.Web.HttpContext.Current.Session["USER"] as LecturerModel;

        [HttpGet]
        public ActionResult Edit()
        {

            if (Session["USER"] != null)
            {
                return View(lecturer);
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Edit(FormCollection form)
        {
            Validation.Validation validation = new Validation.Validation();

            if (validation.CheckString(form["Username"].ToString()) 
                && validation.CheckString(form["Password"].ToString())
                && validation.CheckString(form["FirstName"].ToString())
                && validation.CheckString(form["LastName"].ToString())
                && validation.CheckNum(form["ProfessionalLevel"].ToString())
                && validation.CheckPhoneNumber(form["Phone"].ToString())
                && validation.IsValidEmail(form["Email"].ToString()) && validation.checknull(form["Email"].ToString()))
            {
                lecturer.Username = form["Username"].ToString();
                lecturer.Password = form["Password"].ToString();
                lecturer.Name = form["FirstName"].ToString();
                lecturer.Surname = form["LastName"].ToString();
                lecturer.Level = int.Parse(form["ProfessionalLevel"].ToString());
                lecturer.Cell = form["Phone"].ToString();
                lecturer.Email = form["Email"].ToString();

                lecturer.ChangeAccount(new List<string> { " id = " + lecturer.AccId });
                lecturer.ChangeLecturer(new List<string> { "id = " + lecturer.LectID });
            }

            

            if (Session["USER"] != null)
            {
                return View(lecturer);
            }

            return RedirectToAction("Index", "Home");
        }


        //[HttpPost]

        //public ActionResult SetLecturerProfile(LecturerModel lecturer)
        //{
        //    LecturerModel lect = new LecturerModel();


        //    lect.LectID = lecturer.LectID;
        //    lect.Surname = lecturer.Surname;
        //    lect.Name = lecturer.Name;
        //    lect.Email = lecturer.Email;
        //    lect.Level = lecturer.Level;
        //    lect.Cell = lecturer.Cell;
        //    //
        //    //o.Lecturers.Add(lect);
        //    //o.SaveChanges();
        //    return View();

        //}
        ////
        //// GET: /Profile/Details/5

        //public ActionResult ShowProfileLect(int id)
        //{
        //    //var item = o.LecturerCourseSubjects.ToList();
        //    //return View(item);
        //    return View();
        //}

        //// GET: /Profile/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    //var item = o.Lecturers.Where(x => x.LecturerID == id).First();
        //    //return View(item);
        //    return View();
        //}






        //Editing Lecturer Profile

        //[HttpPost]
        //public ActionResult Edit(FormCollection form)
        //{
        //    LecturerModel lecturer = new LecturerModel();

        //    lecturer = new LecturerModel(form["FirstName"].ToString(), form["LastName"].ToString(), form["Phone"].ToString(), form["Email"].ToString(), int.Parse(form["ProfessionalLevel"].ToString()), form["Username"].ToString(), form["Password"].ToString(), int.Parse(form["Priority"].ToString()), int.Parse(form["Adminstrator"].ToString()));
        //    List<string> accountId = new List<string>();
        //    if (lecturer.ChangeLecturer(accountId))
        //    {
        //        AccountModel account = new AccountModel();
        //        List<AccountModel> accountList = account.GetAccount(null, null);

        //        account = accountList[accountList.Count - 1];
        //        lecturer.AccId = account.AccId;

        //        if (lecturer.ChangeAccount(accountId))
        //        {
        //            return RedirectToAction("Home");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Edit");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Edit");


        //        //  return View("~/Views/Profile/Edit.cshtml", lecturer);
        //    }
        //}



        //[HttpPost]
        //public ActionResult Edit(FormCollection form)
        //{
        //    LecturerModel lecturer = new LecturerModel();

        //    //lecturer = new LecturerModel(int.Parse(form["IdNumber"].ToString()), form["FirstName"].ToString(), form["LastName"].ToString(), form["Phone"].ToString(), form["Email"].ToString());
        //    //lecturer.UpdateLecturerAccount(lecturer);

        //    //List<string> accountId = new List<string>();
        //    //if (lecturer.ChangeLecturer(accountId))
        //    //{
        //    //    AccountModel account = new AccountModel();
        //    //    List<AccountModel> accountList = account.GetAccount(null, null);

        //    //    account = accountList[accountList.Count - 1];
        //    //    lecturer.AccId = account.AccId;

        //    //    if (lecturer.ChangeAccount(accountId))
        //    //    {
        //    //        return RedirectToAction("Home");
        //    //    }
        //    //    else
        //    //    {
        //    //        return RedirectToAction("Edit");
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return RedirectToAction("Edit");


        //    return View("~/Views/Home/Index.cshtml", lecturer);

        //}

        //old work

        //public ActionResult LecturerProfile()
        //{
        //    return View();
        //}
        //[HttpPost]



        //public ActionResult SetLecturerProfile(LecturerModel lecturer)
        //{
        //    LecturerModel lect = new LecturerModel();


        //    lect.LectID = lecturer.LectID;
        //    lect.Surname = lecturer.Surname;
        //    lect.Name = lecturer.Name;
        //    lect.Email = lecturer.Email;
        //    lect.Level = lecturer.Level;
        //    lect.Cell = lecturer.Cell;
        //    //
        //    //o.Lecturers.Add(lect);
        //    //o.SaveChanges();
        //    return View();

        //}
        ////
        //// GET: /Profile/Details/5

        //public ActionResult ShowProfileLect(int id)
        //{
        //    //var item = o.LecturerCourseSubjects.ToList();
        //    //return View(item);
        //    return View();
        //}

        //// GET: /Profile/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    //var item = o.Lecturers.Where(x => x.LecturerID == id).First();
        //    //return View(item);
        //    return View();
        //}


        //public ActionResult Edit(LecturerModel lecturer)
        //{
        //    LecturerModel lect = new LecturerModel();
        //    //var item = o.Lecturers.Where(x => x.LecturerID == lecturer.LecturerID).First();

        //    lect.LectID = lecturer.LectID;
        //    lect.Surname = lecturer.Surname;
        //    lect.Name = lecturer.Name;
        //    lect.Email = lecturer.Email;
        //    lect.Level = lecturer.Level;
        //    lect.Cell = lecturer.Cell;
        //    //o.SaveChanges();
        //    return View(lect);
        //}

        //[HttpPost]

        ////
        //// GET: /Profile/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    //var item = o.Lecturers.Where(x => x.LecturerID == id).First();
        //    //o.Lecturers.Remove(item);
        //    //o.SaveChanges();
        //    //var item2 = o.Lecturers.ToList();
        //    //return View("Show lectuers left", item2);
        //    return View();

        //}

    }
}