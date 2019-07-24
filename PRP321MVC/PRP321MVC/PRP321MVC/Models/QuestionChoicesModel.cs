
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class QuestionChoicesModel : QuestionModel
    {

        // still thinking on this class

        private int id;
        private string choice;

        #region Properties & Constructors
        public QuestionChoicesModel()
            : base()
        {
            ChoiceId = 0;
            Choice = "";
        }

        public QuestionChoicesModel(int cChoiceId)
            : base()
        {
            ChoiceId = cChoiceId;
            Choice = "";
        }

        public QuestionChoicesModel(int cQuestionId, string cChoice)
            : base(cQuestionId)
        {
            ChoiceId = 0;
            Choice = cChoice;
        }

        internal QuestionChoicesModel(int cChoiceId, int cQuestionId, string cChoice)
            : base(cQuestionId)
        {
            ChoiceId = cChoiceId;
            Choice = cChoice;
        }

        public QuestionChoicesModel(string cChoice, int cOutcomeDetailsId, int cQuestionTypeId, string cQuestion, int cWeight, string cOutcomeName, string cCode)
            : base(cOutcomeDetailsId, cQuestionTypeId, cQuestion, cWeight, cOutcomeName, cCode)
        {
            Choice = cChoice;
        }

        public string Choice
        {
            get { return choice; }
            set { choice = value; }
        }

        public int ChoiceId
        {
            get { return id; }
            set { id = value; }
        }
        #endregion 

        #region CRUD
        public bool AddQuestionChoice()
        {
            bool isInserted = false;
            string tableName = "Choices";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("QuestionID");
            columns.Add("Choice");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeQuestionChoice(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "Choices";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("QuestionID", colValues[0]);
            colValuePairs.Add("Choice", colValues[1]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveQuestionChoice(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "Choices";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public List<QuestionChoicesModel> GetQuestionChoice(List<string> columns = null, List<string> where = null)
        {
            string tableName = "Choices";
            List<QuestionChoicesModel> questionChoices = new List<QuestionChoicesModel>();
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
                            fieldValues.Add("QuestionID", attribute);
                            break;
                        case 3:
                            fieldValues.Add("Choice", attribute);
                            break;
                    }
                    counter++;
                }
                questionChoices.Add(ConvertToQuestionChoice(fieldValues));
            }
            return questionChoices;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(QuestionChoicesModel qc)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add(qc.QuestionId.ToString());
                colValues.Add("'" + qc.Choice + "'");
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static QuestionChoicesModel ConvertToQuestionChoice(Dictionary<string, string> fieldValues)
        {
            QuestionChoicesModel qc = new QuestionChoicesModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            qc.ChoiceId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "QuestionID":
                        try
                        {
                            qc.QuestionId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "Choice":
                        qc.Choice = row.Value;
                        break;
                    default:
                        break;
                }
            }
            return qc;
        }
        #endregion
    }
}