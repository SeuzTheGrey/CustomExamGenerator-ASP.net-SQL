using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class PracticalWeightModel
    {
        private int id;
        private int quesionId;
        private int questionWeight;
        private string questionBreakdown;

        public PracticalWeightModel()
        {

        }

        public PracticalWeightModel(int cId, int cQuestionId, int cQuestionWeight, string cQuestionBreakdown)
        {
            id = cId;
            quesionId = cQuestionId;
            questionWeight = cQuestionWeight;
            questionBreakdown = cQuestionBreakdown;
        }

        public string QuestionBreakdown
        {
            get { return questionBreakdown; }
            set { questionBreakdown = value; }
        }

        public int QuestionWeight
        {
            get { return questionWeight; }
            set { questionWeight = value; }
        }

        public int QuestionId
        {
            get { return quesionId; }
            set { quesionId = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public static List<PracticalWeightModel> Select(string question)
        {
            DataAccess.DataHandler datahandler = new DataAccess.DataHandler();
            List<PracticalWeightModel> practicalWeightsList = new List<PracticalWeightModel>();
            DataTable datatable = new DataTable();

            string query = "select * from PracticalWeight where QuestionId = " + question;

            

            using (datatable = datahandler.Select(query))
            {
                foreach (DataRow item in datatable.Rows)
                {
                    practicalWeightsList.Add(new PracticalWeightModel(int.Parse(item["id"].ToString()), int.Parse(item["QuestionId"].ToString()), int.Parse(item["practicalQuestionWeight"].ToString()),item["QuestionBreakDown"].ToString()));
                }
            }

            return practicalWeightsList;
        }
    }
}