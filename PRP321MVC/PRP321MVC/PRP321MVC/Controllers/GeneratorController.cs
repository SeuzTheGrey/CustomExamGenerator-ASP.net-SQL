using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRP321MVC.Models;

namespace PRP321MVC.Controllers
{
    public class GeneratorController : Controller
    {
        LecturerModel lecturer = System.Web.HttpContext.Current.Session["USER"] as LecturerModel;
        PRP321MVC.Models.GeneratorModel GeneratorModel = new Models.GeneratorModel();

        public ActionResult Create()
        {

            if (Session["USER"] != null)
            {
                if (lecturer.Priority >= 7)
                {
                    DownloadFile download = new DownloadFile();
                    List<DownloadFile> files = download.GetFiles();
                    return View(files);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public FileResult DownloadFile(string filepath)
        {
            return File(filepath, "application/docx");
        }


        public ActionResult SelectSubjects()
        {
            if (Session["USER"] != null)
            {
                if (lecturer.Priority >= 7)
                {
                    Session["Generator"] = GeneratorModel;
                    List<SubjectModel> SubjectList = SubjectModel.GetSubject(null, null);
                    return View(SubjectList);
                }
            }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult SelectTestNumber(FormCollection form)
        {

            if (Session["USER"] != null)
            {
                if (lecturer.Priority >= 7)
                {
                    string Subject = form["Subject"].ToString();

                    HttpCookie SubjectChoosen = new HttpCookie("SubjectChoosen", Subject);
                    Response.Cookies.Add(SubjectChoosen);
                    List<string> where = new List<string>() { "name = " + "'" + form["Subject"].ToString() + "'" };

                    List<SubjectModel> SubjectList = SubjectModel.GetSubject(null, where);

                    SubjectModel subject = new SubjectModel();

                    if (SubjectList.Count == 1)
                    {
                        foreach (SubjectModel item in SubjectList)
                        {
                            subject = item;
                        }
                    }
                    else
                    {
                        throw new CustomException.CustomException("Something Went Wrong");
                    }

                    GeneratorModel generator = Session["Generator"] as GeneratorModel;

                    generator.Subject = subject;

                    Session["Generator"] = generator;
                    return View(subject);
                }
            }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult SelectNumberOfQuestions(FormCollection form)
        {

            if (Session["USER"] != null)
            {
                if (lecturer.Priority >= 7)
                {
                    GeneratorModel generator = Session["Generator"] as GeneratorModel;
                    generator.TestType = form["TestType"].ToString();
                    Session["Generator"] = generator;

                    return View();
                }
            }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult SelectQuestions(FormCollection form)
        {

            if (Session["USER"] != null)
            {
                if (lecturer.Priority >= 7)
                {
                    GeneratorModel generator = Session["Generator"] as GeneratorModel;
                    generator.Examinator = form["Examinator"].ToString();
                    generator.Moderator = form["Moderator"].ToString();
                    generator.NumberOfQuestions = int.Parse(form["QuestionQuantity"].ToString());

                    QuestionsSelectionModel questionsSelectionModel = new QuestionsSelectionModel(Request.Cookies["SubjectChoosen"].Value);

                    //for (int i = 0; i < questionsSelectionModel.NumberOfQuestions; i++)
                    //{
                    //    List<SelectListItem> listItems = new List<SelectListItem>();

                    //    foreach (QuestionTypeModel item in questionsSelectionModel.questionTypesList)
                    //    {
                    //        listItems.Add(new SelectListItem { Text = item.Name, Value = item.Type });
                    //    }

                    //    string QuestionTypei = "QuestionType" + i;

                    //    ViewBag.QuestionTypei = listItems;

                    //}
                    questionsSelectionModel.NumberOfQuestions = generator.NumberOfQuestions;

                    Session["Generator"] = generator;
                    return View(questionsSelectionModel);
                }
            }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult SelectOutcomes(FormCollection form)
        {

            if (Session["USER"] != null)
            {
                if (lecturer.Priority >= 7)
                {
                    GeneratorModel generator = Session["Generator"] as GeneratorModel;
                    string[] TypesOfQuestions = new string[generator.NumberOfQuestions];


                    for (int i = 0; i < generator.NumberOfQuestions; i++)
                    {
                        TypesOfQuestions[i] = form["QuestionType" + i].ToString();
                    }



                    QuestionsSelectionModel questionsSelectionModel = new QuestionsSelectionModel(Request.Cookies["SubjectChoosen"].Value);

                    questionsSelectionModel.NumberOfQuestions = generator.NumberOfQuestions;

                    questionsSelectionModel.TypesOfQuestion = TypesOfQuestions;

                    //Session["Selection"] = questionsSelectionModel;

                    generator.TypesOfQuestions = TypesOfQuestions;

                    Session["Generator"] = generator;

                    return View(questionsSelectionModel);
                }
            }

            return RedirectToAction("Index", "Home");


        }

        [HttpPost]
        public ActionResult FinishUp(FormCollection form)
        {
            //QuestionsSelectionModel obj = new QuestionsSelectionModel();
            if (Session["USER"] != null)
            {
                if (lecturer.Priority >= 7)
                {
                    GeneratorModel generator = Session["Generator"] as GeneratorModel;
                    generator.lecturerName = lecturer.Username;

                    List<OutcomesCollectionModel> outcomeCollection = new List<OutcomesCollectionModel>();
                    int[] numberOfQuestionsPerQuestion = new int[generator.NumberOfQuestions];

                    for (int i = 0; i < generator.NumberOfQuestions; i++)
                    {
                        List<string> outcomes = new List<string>();



                        numberOfQuestionsPerQuestion[i] = int.Parse(form["NumberOFQuestionsPerQuestion " + i].ToString());

                        List<OutcomeDetailsModel> outcomesList = OutcomeDetailsModel.GetOutcomeDetails(null, null);


                        outcomes.Add(form["OutcomeChoosen" + i].ToString());


                        outcomeCollection.Add(new OutcomesCollectionModel(i, generator.TypesOfQuestions[i], outcomes));
                    }

                    generator.outcomesForWhere = outcomeCollection;
                    generator.NumberOfQuestionsPerQuestion = numberOfQuestionsPerQuestion;

                    generator.CreateExam();

                    Session["Generator"] = generator;

                    return RedirectToAction("Create", "Generator");
                }
            }

            return RedirectToAction("Index", "Home");


        }
    }
}