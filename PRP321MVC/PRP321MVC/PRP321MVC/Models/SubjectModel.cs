
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class SubjectModel
    {
        private int id;
        private string name;
        private string code;
        private int duration;
        private int weight;
        private string description;

        #region Properties & Constructors
        public SubjectModel()
        {
            SubjectId = 0;
            Name = "";
            Code = "";
            Duration = 0;
            Weight = 0;
            Description = "";
        }

        public SubjectModel(int cId)
        {
            SubjectId = cId;
            Name = "";
            Code = "";
            Duration = 0;
            Weight = 0;
            Description = "";
        }

        public SubjectModel(string cName, string cCode, int cDuration, int cWeight, string cDescription)
        {
            SubjectId = 0;
            Name = cName;
            Code = cCode;
            Duration = cDuration;
            Weight = cWeight;
            Description = cDescription;
        }

        internal SubjectModel(int cId, string cName, string cCode, int cDuration, int cWeight, string cDescription)
        {
            SubjectId = cId;
            Name = cName;
            Code = cCode;
            Duration = cDuration;
            Weight = cWeight;
            Description = cDescription;
        }

        public SubjectModel(string cCode)
        {
            SubjectId = 0;
            Name = "";
            Code = cCode;
            Duration = 0;
            Weight = 0;
            Description = "";
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int SubjectId
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region CRUD
        public bool AddSubject()
        {
            bool isInserted = false;
            string tableName = "Subjects";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("name");
            columns.Add("code");
            columns.Add("duration");
            columns.Add("SubjectWeight");
            columns.Add("SubjectDescription");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeSubjects(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "Subjects";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("name", colValues[0]);
            colValuePairs.Add("code", colValues[1]);
            colValuePairs.Add("duration", colValues[2]);
            colValuePairs.Add("SubjectWeight", colValues[3]);
            colValuePairs.Add("SubjectDescription", colValues[4]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveSubject(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "Subjects";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public static List<SubjectModel> GetSubject(List<string> columns = null, List<string> where = null)
        {
            string tableName = "Subjects";
            List<SubjectModel> subjects = new List<SubjectModel>();
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
                            fieldValues.Add("code", attribute);
                            break;
                        case 4:
                            fieldValues.Add("duration", attribute);
                            break;
                        case 5:
                            fieldValues.Add("SubjectWeight", attribute);
                            break;
                        case 6:
                            fieldValues.Add("SubjectDescription", attribute);
                            break;
                    }
                    counter++;
                }
                subjects.Add(ConvertToSubject(fieldValues));
            }
            return subjects;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(SubjectModel s)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add("'" + s.Name + "'");
                colValues.Add("'" + s.Code + "'");
                colValues.Add(s.Duration.ToString());
                colValues.Add(s.Weight.ToString());
                colValues.Add("'" + s.Description + "'");
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static SubjectModel ConvertToSubject(Dictionary<string, string> fieldValues)
        {
            SubjectModel s = new SubjectModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            s.SubjectId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "name":
                        s.Name = row.Value;
                        break;
                    case "code":
                        s.Code = row.Value;
                        break;
                    case "duration":
                        try
                        {
                            s.Duration = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "SubjectWeight":
                        try
                        {
                            s.Weight = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "SubjectDescription":
                        s.Description = row.Value;
                        break;
                    default:
                        break;
                }
            }
            return s;
        }
        #endregion
    }
}