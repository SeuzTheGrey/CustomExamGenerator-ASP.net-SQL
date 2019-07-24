using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class CourseModel
    {
        private int id;
        private string courseCode;
        private string courseName;
        private int nqfLevel;
        private string courseDescription;

        #region Properties & Constructors
        public CourseModel()
        {
            CourseId = 0;
            CourseCode = "";
            CourseName = "";
            NQFLevel = 0;
            CourseDescription = "";
        }

        public CourseModel(int cCourseId)
        {
            CourseId = cCourseId;
            CourseCode = "";
            CourseName = "";
            NQFLevel = 0;
            CourseDescription = "";
        }

        public CourseModel(string cCourseCode, string cCourseName, int cNQFLevel, string cCourseDescription)
        {
            CourseId = 0;
            CourseCode = cCourseCode;
            CourseName = cCourseName;
            NQFLevel = cNQFLevel;
            CourseDescription = cCourseDescription;
        }

        internal CourseModel(int cCourseId, string cCourseCode, string cCourseName, int cNQFLevel, string cCourseDescription)
        {
            CourseId = cCourseId;
            CourseCode = cCourseCode;
            CourseName = cCourseName;
            NQFLevel = cNQFLevel;
            CourseDescription = cCourseDescription;
        }

        public string CourseDescription
        {
            get { return courseDescription; }
            set { courseDescription = value; }
        }

        public int NQFLevel
        {
            get { return nqfLevel; }
            set { nqfLevel = value; }
        }

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }

        public string CourseCode
        {
            get { return courseCode; }
            set { courseCode = value; }
        }

        public int CourseId
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region CRUD
        public bool AddCourse()
        {
            bool isInserted = false;
            string tableName = "Course";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("code");
            columns.Add("name");
            columns.Add("NQFLevel");
            columns.Add("CourseDescription");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeCourse(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "Course";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("code", colValues[0]);
            colValuePairs.Add("name", colValues[1]);
            colValuePairs.Add("NQFLevel", colValues[2]);
            colValuePairs.Add("CourseDescription", colValues[3]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveCourse(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "Course";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public static List<CourseModel> GetCourse(List<string> columns = null, List<string> where = null)
        {
            string tableName = "Course";
            List<CourseModel> courses = new List<CourseModel>();
            List<string> records = new List<string>();
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            records = dh.Select(tableName, columns, where);
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();

            foreach (string item in records)
            {
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
                            fieldValues.Add("code", attribute);
                            break;
                        case 3:
                            fieldValues.Add("name", attribute);
                            break;
                        case 4:
                            fieldValues.Add("NQFLevel", attribute);
                            break;
                        case 5:
                            fieldValues.Add("CourseDescription", attribute);
                            break;
                    }
                    counter++;
                }
                courses.Add(ConvertToCourse(fieldValues));
            }
            return courses;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(CourseModel c)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add("'" + c.CourseCode + "'");
                colValues.Add("'" + c.CourseName + "'");
                colValues.Add(c.NQFLevel.ToString());
                colValues.Add("'" + c.CourseDescription + "'");
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static CourseModel ConvertToCourse(Dictionary<string, string> fieldValues)
        {
            CourseModel c = new CourseModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            c.CourseId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "code":
                        c.CourseCode = row.Value;
                        break;
                    case "name":
                        c.CourseName = row.Value;
                        break;
                    case "NQFLevel":
                        try
                        {
                            c.NQFLevel = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "CourseDescription":
                        c.CourseDescription = row.Value;
                        break;
                    default:
                        break;
                }
            }
            return c;
        }
        #endregion
    }
}