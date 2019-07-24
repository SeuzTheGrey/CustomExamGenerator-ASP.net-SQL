
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class QuestionModel : OutcomeDetailsModel
    {
        private int questionId;
        private int questionTypeId;
        private string questions;
        private int weight;

        #region Properties & Constructors
        public QuestionModel()
            : base()
        {
            QuestionId = 0;
            QuestionTypeId = 0;
            Questions = "";
            Weight = 0;
        }

        public QuestionModel(int cQuestionId)
            : base()
        {
            QuestionId = cQuestionId;
            QuestionTypeId = 0;
            Questions = "";
            Weight = 0;
        }

        internal QuestionModel(int cQuestionId, int cOutcomeDetailsId, int cQuestionTypeId, string cQuestion, int cWeight)
            : base(cOutcomeDetailsId)
        {
            QuestionId = cQuestionId;
            QuestionTypeId = cQuestionTypeId;
            Questions = cQuestion;
            Weight = cWeight;
        }

        public QuestionModel(int cOutcomeDetailsId, int cQuestionTypeId, string cQuestion, int cWeight, string cOutcomeName, string cCode)
            : base(cOutcomeDetailsId)
        {
            QuestionTypeId = cQuestionTypeId;
            Questions = cQuestion;
            Weight = cWeight;
            base.OutcomeName = cOutcomeName;
            base.Code = cCode;
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string Questions
        {
            get { return questions; }
            set { questions = value; }
        }

        public int QuestionTypeId
        {
            get { return questionTypeId; }
            set { questionTypeId = value; }
        }

        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; }
        }
        #endregion

        #region CRUD
        public bool AddQuestion(QuestionModel obj)
        {
            bool isInserted = false;
            string tableName = "Question";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(obj);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("OutcomeDetailsID");
            columns.Add("QuestionTypeID");
            columns.Add("Questions");
            columns.Add("QuestionWeight");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeQuestion(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "Question";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("OutcomeDetailsID", colValues[0]);
            colValuePairs.Add("QuestionTypeID", colValues[1]);
            colValuePairs.Add("Questions", colValues[2]);
            colValuePairs.Add("QuestionWeight", colValues[3]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveQuestion(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "Question";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public List<QuestionModel> GetQuestion(List<string> columns = null, List<string> where = null)
        {
            string tableName = "Question";
            List<QuestionModel> questions = new List<QuestionModel>();
            List<string> records = new List<string>();
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            records = dh.Select(tableName, columns, where);
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();

            foreach (string item in records)
            {
                fieldValues.Clear();
                int counter = 1;
                string[] fields = item.Split(',');
                foreach (string attribute in fields)
                {
                    switch (counter)
                    {
                        case 1:
                            fieldValues.Add("id", attribute);
                            break;
                        case 2:
                            fieldValues.Add("OutcomeDetailsID", attribute);
                            break;
                        case 3:
                            fieldValues.Add("QuestionTypeID", attribute);
                            break;
                        case 4:
                            fieldValues.Add("Questions", attribute);
                            break;
                        case 5:
                            fieldValues.Add("QuestionWeight", attribute);
                            break;
                    }
                    counter++;
                }
                questions.Add(ConvertToQuestion(fieldValues));
            }
            return questions;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(QuestionModel q)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add(q.OutcomeDetailsId.ToString());
                colValues.Add(q.QuestionTypeId.ToString());
                colValues.Add("'" + q.Questions + "'");
                colValues.Add(q.Weight.ToString());
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static QuestionModel ConvertToQuestion(Dictionary<string, string> fieldValues)
        {
            QuestionModel q = new QuestionModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            q.QuestionId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "OutcomeDetailsID":
                        try
                        {
                            q.OutcomeDetailsId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "QuestionTypeID":
                        try
                        {
                            q.QuestionTypeId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "Questions":
                        q.Questions = row.Value;
                        break;
                    case "QuestionWeight":
                        try
                        {
                            q.Weight = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
            return q;
        }
        #endregion

        public List<QuestionModel> Select(List<string> outcomes)
        {
            DataAccess.DataHandler datahandler = new DataAccess.DataHandler();
            List<QuestionModel> questionsList = new List<QuestionModel>();
            DataTable datatable = new DataTable();

            string query = "select * from Question inner join OutcomeDetails on OutcomeDetails.id = Question.OutcomeDetailsID where ";

            for (int i = 0; i < outcomes.Count; i++)
            {
                query = query + "OutcomeDetails.Material = '" + outcomes[i] + "'";
                if (i <= outcomes.Count - 1)
                {

                }
                else
                {
                    query = query + " or ";
                }
            }

            using (datatable = datahandler.Select(query))
            {
                foreach (DataRow item in datatable.Rows)
                {
                    questionsList.Add(new QuestionModel(int.Parse(item["id"].ToString()), int.Parse(item["OutcomeDetailsID"].ToString()), int.Parse(item["QuestionTypeID"].ToString()), item["Questions"].ToString(), int.Parse(item["QuestionWeight"].ToString())));
                }
            }

            return questionsList;
        }
    }
}