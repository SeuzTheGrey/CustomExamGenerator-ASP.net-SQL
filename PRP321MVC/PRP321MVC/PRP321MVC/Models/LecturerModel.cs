using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class LecturerModel : AccountModel
    {
        private int id;
        private string name;
        private string surname;
        private string cell;
        private string email;
        private int level;

        #region Properties & Constructors
        public LecturerModel()
            : base()
        {
            LectID = 0;
            Name = "";
            Surname = "";
            Cell = "";
            Email = "";
            Level = 0;
        }

        public LecturerModel(int cLecturerId)
            : base()
        {
            LectID = cLecturerId;
            Name = "";
            Surname = "";
            Cell = "";
            Email = "";
            Level = 0;
        }

        public LecturerModel(string cUsername, string cPassword)
            : base(cUsername, cPassword)
        {
            LectID = 0;
            Name = "";
            Surname = "";
            Cell = "";
            Email = "";
            Level = 0;
        }

        public LecturerModel(int cAccountId, string cName, string cSurname, string cCell, string cEmail, int cLevel)
            : base(cAccountId)
        {
            LectID = 0;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        internal LecturerModel(int cLecturerId, int cAccountId, string cName, string cSurname, string cCell, string cEmail, int cLevel)
            : base(cAccountId)
        {
            LectID = cLecturerId;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        public LecturerModel(string cUsername, string cPassword, int cPriority, int cIsAdmin)
            : base(cUsername, cPassword, cPriority, cIsAdmin)
        {
            LectID = 0;
            Name = "";
            Surname = "";
            Cell = "";
            Email = "";
            Level = 0;
        }

        public LecturerModel(string cName, string cSurname, string cCell, string cEmail, int cLevel, string cUsername, string cPassword, int cPriority, int cIsAdmin)
            : base(cUsername, cPassword, cPriority, cIsAdmin)
        {
            LectID = 0;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        internal LecturerModel(int cId, string cName, string cSurname, string cCell, string cEmail, int cLevel, string cUsername, string cPassword, int cPriority, int cIsAdmin)
            : base(cUsername, cPassword, cPriority, cIsAdmin)
        {
            LectID = cId;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        internal LecturerModel(int cId, string cName, string cSurname, string cCell, string cEmail, int cLevel, int cACCId, string cUsername, string cPassword, int cPriority, int cIsAdmin)
            : base(cACCId,cUsername, cPassword, cPriority, cIsAdmin)
        {
            LectID = cId;
            Name = cName;
            Surname = cSurname;
            Cell = cCell;
            Email = cEmail;
            Level = cLevel;
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Cell
        {
            get { return cell; }
            set { cell = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int LectID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region CRUD
        public bool AddLecturer()
        {
            bool isInserted = false;
            string tableName = "Lecturer";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("AccountID");
            columns.Add("name");
            columns.Add("surname");
            columns.Add("cell");
            columns.Add("email");
            columns.Add("LecturerLevel");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeLecturer(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "Lecturer";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("AccountID", colValues[0]);
            colValuePairs.Add("name", colValues[1]);
            colValuePairs.Add("surname", colValues[2]);
            colValuePairs.Add("cell", colValues[3]);
            colValuePairs.Add("email", colValues[4]);
            colValuePairs.Add("LecturerLevel", colValues[5]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveLecturer(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "Lecturer";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public List<LecturerModel> GetLecturer(List<string> columns = null, List<string> where = null)
        {
            string tableName = "Lecturer";
            List<LecturerModel> lecturers = new List<LecturerModel>();
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
                            fieldValues.Add("AccountID", attribute);
                            break;
                        case 3:
                            fieldValues.Add("name", attribute);
                            break;
                        case 4:
                            fieldValues.Add("surname", attribute);
                            break;
                        case 5:
                            fieldValues.Add("cell", attribute);
                            break;
                        case 6:
                            fieldValues.Add("email", attribute);
                            break;
                        case 7:
                            fieldValues.Add("LecturerLevel", attribute);
                            break;
                    }
                    counter++;
                }
                lecturers.Add(ConvertToLecturer(fieldValues));
            }
            return lecturers;
        }

        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(LecturerModel l)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add(l.AccId.ToString());
                colValues.Add("'" + l.Name + "'");
                colValues.Add("'" + l.Surname + "'");
                colValues.Add("'" + l.Cell + "'");
                colValues.Add("'" + l.Email + "'");
                colValues.Add(l.Level.ToString());
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static LecturerModel ConvertToLecturer(Dictionary<string, string> fieldValues)
        {
            LecturerModel l = new LecturerModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            l.LectID = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "AccountID":
                        try
                        {
                            l.AccId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "name":
                        l.Name = row.Value;
                        break;
                    case "surname":
                        l.Surname = row.Value;
                        break;
                    case "cell":
                        l.Cell = row.Value;
                        break;
                    case "email":
                        l.Email = row.Value;
                        break;
                    case "LecturerLevel":
                        try
                        {
                            l.Level = int.Parse(row.Value);
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
            return l;
        }
        #endregion
    }
}