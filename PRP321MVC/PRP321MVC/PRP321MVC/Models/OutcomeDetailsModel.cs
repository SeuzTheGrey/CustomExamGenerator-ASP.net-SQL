


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class OutcomeDetailsModel : OutcomeModel
    {
        private int outcomeDetailsId;
        private string material;

        #region Properties & Constructors
        public OutcomeDetailsModel()
            : base()
        {
            OutcomeDetailsId = 0;
            Material = "";
        }

        public OutcomeDetailsModel(int cOutcomeDetailsId)
            : base()
        {
            OutcomeDetailsId = cOutcomeDetailsId;
            Material = "";
        }

        public OutcomeDetailsModel(int cOutcomeId, string cMaterial)
            : base(cOutcomeId)
        {
            OutcomeDetailsId = 0;
            Material = cMaterial;
        }

        internal OutcomeDetailsModel(int cOutcomeDetailsId, int cOutcomeId, string cMaterial)
            : base(cOutcomeId)
        {
            OutcomeDetailsId = cOutcomeDetailsId;
            Material = cMaterial;
        }

        public OutcomeDetailsModel(int cOutcomeDetailsId, int cOutcomeId, string cMaterial, string cOutcomeName)
            :base(cOutcomeId,cOutcomeName)
        {
            OutcomeDetailsId = cOutcomeDetailsId;
            Material = cMaterial;
        }

        public string Material
        {
            get { return material; }
            set { material = value; }
        }

        public int OutcomeDetailsId
        {
            get { return outcomeDetailsId; }
            set { outcomeDetailsId = value; }
        }
        #endregion

        #region CRUD
        public bool AddOutcomeDetails()
        {
            bool isInserted = false;
            string tableName = "OutcomeDetails";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("OutcomeID");
            columns.Add("Material");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeOutcomeDetails(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "OutcomeDetails";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("OutcomeID", colValues[0]);
            colValuePairs.Add("Material", colValues[1]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveOutcomeDetails(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "OutcomeDetails";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public static List<OutcomeDetailsModel> GetOutcomeDetails(List<string> columns = null, List<string> where = null)
        {
            string tableName = "OutcomeDetails";
            List<OutcomeDetailsModel> outcomeDetails = new List<OutcomeDetailsModel>();
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
                            fieldValues.Add("OutcomeID", attribute);
                            break;
                        case 3:
                            fieldValues.Add("Material", attribute);
                            break;
                    }
                    counter++;
                }
                outcomeDetails.Add(ConvertToOutcomeDetails(fieldValues));
            }
            return outcomeDetails;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(OutcomeDetailsModel od)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add(od.OutcomeId.ToString());
                colValues.Add("'" + od.Material + "'");
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static OutcomeDetailsModel ConvertToOutcomeDetails(Dictionary<string, string> fieldValues)
        {
            OutcomeDetailsModel od = new OutcomeDetailsModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            od.OutcomeDetailsId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "OutcomeID":
                        try
                        {
                            od.OutcomeId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "Material":
                        od.Material = row.Value;
                        break;
                    default:
                        break;
                }
            }
            return od;
        }
        #endregion
    }
}