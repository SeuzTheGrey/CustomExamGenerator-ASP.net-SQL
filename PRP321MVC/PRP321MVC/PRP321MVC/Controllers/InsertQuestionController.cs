using PRP321MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRP321MVC.Controllers
{
    public class InsertQuestionController : Controller
    {

        LecturerModel lecturer = System.Web.HttpContext.Current.Session["USER"] as LecturerModel;

        // GET: InsertQuestion
        public ActionResult Index()
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


        //calling when we hit the controller
        public ActionResult InsertDetails()
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

        //calling on the HTTP post ( Submit)

        [HttpPost]
        public ActionResult InsertDetails(QuestionModel qm)
        {

            if (Session["USER"] != null)
            {
                if (lecturer.IsAdmin == 1)
                {
                    QuestionModel questionModel = new QuestionModel();
                    bool result = questionModel.AddQuestion(qm);

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

            return RedirectToAction("Index", "Home");

            
        }
    }
}