
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class OutcomeModel : SubjectModel
    {
        private int id;
        private string outcomeName;

        #region Properties & Constructors
        public OutcomeModel()
            : base()
        {
            OutcomeId = 0;
            OutcomeName = "";
        }

        public OutcomeModel(int cOutcomeId)
            : base()
        {
            OutcomeId = cOutcomeId;
            OutcomeName = "";
        }

        //public OutcomeModel(int cSubjectId, string cOutcomeName)
        //    : base(cSubjectId)
        //{
        //    OutcomeId = 0;
        //    OutcomeName = cOutcomeName;
        //}

        internal OutcomeModel(int cOutcomeId, int cSubjectId, string cOutcomeName)
            : base(cSubjectId)
        {
            OutcomeId = cOutcomeId;
            OutcomeName = cOutcomeName;
        }

        public OutcomeModel(string cOutcomeName, string cCode)
            : base(cCode)
        {
            OutcomeId = 0;
            OutcomeName = cOutcomeName;
            Code = cCode;
        }

        public OutcomeModel(int cId, string cOutcomeName)
            : base()
        {
            OutcomeId = cId;
            OutcomeName = cOutcomeName;
        }

        public string OutcomeName
        {
            get { return outcomeName; }
            set { outcomeName = value; }
        }

        public int OutcomeId
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region CRUD
        public bool AddOutcome()
        {
            bool isInserted = false;
            string tableName = "Outcome";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("SubjectID");
            columns.Add("name");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeOutcome(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "Outcome";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("SubjectID", colValues[0]);
            colValuePairs.Add("name", colValues[1]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveOutcome(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "Outcome";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public static List<OutcomeModel> GetOutcome(List<string> columns = null, List<string> where = null)
        {
            string tableName = "Outcome";
            List<OutcomeModel> outcomes = new List<OutcomeModel>();
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
                            fieldValues.Add("SubjectID", attribute);
                            break;
                        case 3:
                            fieldValues.Add("name", attribute);
                            break;
                    }
                    counter++;
                }
                outcomes.Add(ConvertToOutcome(fieldValues));
            }
            return outcomes;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(OutcomeModel o)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add(o.SubjectId.ToString());
                colValues.Add("'" + o.OutcomeName + "'");
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static OutcomeModel ConvertToOutcome(Dictionary<string, string> fieldValues)
        {
            OutcomeModel o = new OutcomeModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            o.OutcomeId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "SubjectID":
                        try
                        {
                            o.SubjectId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "name":
                        o.OutcomeName = row.Value;
                        break;
                    default:
                        break;
                }
            }
            return o;
        }
        #endregion
    }
}