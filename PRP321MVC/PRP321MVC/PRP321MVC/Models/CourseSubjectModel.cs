using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class CourseSubjectModel : SubjectModel
    {
        private int courseSubjectId;
        private int courseId;

        #region Properties & Constructors
        public CourseSubjectModel()
            : base()
        {
            CourseSubjectId = 0;
            CourseId = 0;
        }

        public CourseSubjectModel(int cCourseSubjectId)
            : base()
        {
            CourseSubjectId = cCourseSubjectId;
            CourseId = 0;
        }

        public CourseSubjectModel(int cSubjectId, int cCourseId)
            : base(cSubjectId)
        {
            CourseSubjectId = 0;
            CourseId = cCourseId;
        }

        internal CourseSubjectModel(int cCourseSubjectId, int cSubjectId, int cCourseId)
            : base(cSubjectId)
        {
            CourseSubjectId = cCourseSubjectId;
            CourseId = cCourseId;
        }

        public int CourseSubjectId
        {
            get { return courseSubjectId; }
            set { courseSubjectId = value; }
        }

        public int CourseId
        {
            get { return courseId; }
            set { courseId = value; }
        }
        #endregion

        #region CRUD
        public bool AddCourseSubject()
        {
            bool isInserted = false;
            string tableName = "CourseSubject";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("SubjectId");
            columns.Add("CourseId");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeCourseSubject(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "CourseSubject";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("SubjectId", colValues[0]);
            colValuePairs.Add("CourseId", colValues[1]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveCourseSubject(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "CourseSubject";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public static List<CourseSubjectModel> GetCourseSubject(List<string> columns = null, List<string> where = null)
        {
            string tableName = "CourseSubject";
            List<CourseSubjectModel> courseSubjects = new List<CourseSubjectModel>();
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
                            fieldValues.Add("SubjectId", attribute);
                            break;
                        case 3:
                            fieldValues.Add("CourseId", attribute);
                            break;
                    }
                    counter++;
                }
                courseSubjects.Add(ConvertToCourseSubject(fieldValues));
            }
            return courseSubjects;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(CourseSubjectModel cs)
        {
            List<string> colValues = new List<string>();

            try
            {
                colValues.Add(cs.SubjectId.ToString());
                colValues.Add(cs.CourseId.ToString());
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static CourseSubjectModel ConvertToCourseSubject(Dictionary<string, string> fieldValues)
        {
            CourseSubjectModel cs = new CourseSubjectModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            cs.CourseSubjectId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "SubjectId":
                        try
                        {
                            cs.SubjectId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "CourseId":
                        try
                        {
                            cs.CourseId = int.Parse(row.Value);
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
            return cs;
        }
        #endregion
    }
}