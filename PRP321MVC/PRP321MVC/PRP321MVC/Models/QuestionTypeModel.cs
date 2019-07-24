using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class QuestionTypeModel
    {
        private int questionTypeId;
        private string name;
        private string type;

        #region Properties & Constructors
        public QuestionTypeModel()
        {
            QuestionTypeId = 0;
            Name = "";
            Type = "";
        }

        public QuestionTypeModel(int cQuestionTypeId)
        {
            QuestionTypeId = cQuestionTypeId;
            Name = "";
            Type = "";
        }

        public QuestionTypeModel(string cName, string cType)
        {
            QuestionTypeId = 0;
            Name = cName;
            Type = cType;
        }

        internal QuestionTypeModel(int cQuestionTypeId, string cName, string cType)
        {
            QuestionTypeId = cQuestionTypeId;
            Name = cName;
            Type = cType;
        }

        public int QuestionTypeId
        {
            get { return questionTypeId; }
            set { questionTypeId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion

        #region CRUD
        public bool AddQuestionType()
        {
            bool isInserted = false;
            string tableName = "QuestionType";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("name");
            columns.Add("QuestionType");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeQuestionType(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "QuestionType";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("name", colValues[0]);
            colValuePairs.Add("QuestionType", colValues[1]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveQuestionType(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "QuestionType";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public static List<QuestionTypeModel> GetQuestionType(List<string> columns = null, List<string> where = null)
        {
            string tableName = "QuestionsType";
            List<QuestionTypeModel> questionTypes = new List<QuestionTypeModel>();
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
                            fieldValues.Add("name", attribute);
                            break;
                        case 3:
                            fieldValues.Add("QuestionType", attribute);
                            break;
                    }
                    counter++;
                }
                questionTypes.Add(ConvertToQuestionType(fieldValues));
            }
            return questionTypes;
        }
        #endregion 

        #region Data Manipulation
        private List<string> ConvertToColumnValues(QuestionTypeModel qt)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add("'" + qt.Name + "'");
                colValues.Add("'" + qt.Type + "'");
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static QuestionTypeModel ConvertToQuestionType(Dictionary<string, string> fieldValues)
        {
            QuestionTypeModel qt = new QuestionTypeModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            qt.QuestionTypeId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "name":
                        qt.Name = row.Value;
                        break;
                    case "QuestionType":
                        qt.Type = row.Value;
                        break;
                    default:
                        break;
                }
            }
            return qt;
        }
        #endregion
    }
}