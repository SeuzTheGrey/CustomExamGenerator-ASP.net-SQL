using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class AnswerModel
    {
        private int id;
        private int questionId;
        private string answer;

        #region Properties & Constructors
        public AnswerModel()
        {
            Id = 0;
            QuestionId = 0;
            Answers = "";
        }

        public AnswerModel(int cId)
        {
            Id = cId;
            QuestionId = 0;
            Answers = "";
        }

        internal AnswerModel(int cId, int cQuestionId, string cAnswer)
        {
            Id = cId;
            QuestionId = cQuestionId;
            Answers = cAnswer;
        }

        public string Answers
        {
            get { return answer; }
            set { answer = value; }
        }

        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region CRUD

        public bool AddAnswer(AnswerModel amObj)
        {
            bool isInserted = false;
            string tableName = "Answer";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(amObj);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("questionId");
            columns.Add("Answer");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool AddAnswer()
        {
            bool isInserted = false;
            string tableName = "Answer";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("questionId");
            columns.Add("Answer");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeAnswer(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "Answer";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("questionId", colValues[0]);
            colValuePairs.Add("Answer", colValues[1]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveAnswer(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "Answer";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public static List<AnswerModel> GetAnswer(List<string> columns = null, List<string> where = null)
        {
            string tableName = "Answer";
            List<AnswerModel> answers = new List<AnswerModel>();
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
                            fieldValues.Add("questionId", attribute);
                            break;
                        case 3:
                            fieldValues.Add("Answer", attribute);
                            break;
                    }
                    counter++;
                }
                answers.Add(ConvertToAnswer(fieldValues));
            }
            return answers;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(AnswerModel a)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add(a.QuestionId.ToString());
                colValues.Add("'" + a.Answers + "'");
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static AnswerModel ConvertToAnswer(Dictionary<string, string> fieldValues)
        {
            AnswerModel a = new AnswerModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            a.Id = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "questionId":
                        try
                        {
                            a.QuestionId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "Answer":
                        a.Answers = row.Value;
                        break;
                    default:
                        break;
                }
            }
            return a;
        }
        #endregion

        public static List<AnswerModel> Select(List<string> answers)
        {
            DataAccess.DataHandler datahandler = new DataAccess.DataHandler();
            List<AnswerModel> answersList = new List<AnswerModel>();
            DataTable datatable = new DataTable();

            string query = "select * from Answer where ";

            for (int i = 0; i < answers.Count; i++)
            {
                query = query + "questionId = " + answers[i];
                if (i < answers.Count - 1)
                {
                    query = query + " or ";
                }
                else
                {
                    
                }
            }

            using (datatable = datahandler.Select(query))
            {
                foreach (DataRow item in datatable.Rows)
                {
                    answersList.Add(new AnswerModel(int.Parse(item["id"].ToString()), int.Parse(item["questionId"].ToString()), item["Answer"].ToString()));
                }
            }

            return answersList;
        }
    }
}