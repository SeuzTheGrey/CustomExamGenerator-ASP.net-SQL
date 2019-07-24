using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class LecturerCourseSubjectModel : CourseSubjectModel
    {
        private int lcsId;
        private int lecturerId;
        private DateTime startDate;
        private DateTime endDate;
        private int isFlagged;

        #region Properties & Constructors
        public LecturerCourseSubjectModel()
            : base()
        {
            LCSId = 0;
            LecturerId = 0;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public LecturerCourseSubjectModel(int cLecturerCourseSubjectId)
            : base()
        {
            LCSId = cLecturerCourseSubjectId;
            LecturerId = 0;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public LecturerCourseSubjectModel(int cLecturerId, int cCourseSubjectId, DateTime cStartDate, DateTime cEndDate, int cIsFlagged)
            : base(cCourseSubjectId)
        {
            LCSId = 0;
            LecturerId = cLecturerId;
            StartDate = cStartDate;
            EndDate = cEndDate;
            IsFlagged = cIsFlagged;
        }

        internal LecturerCourseSubjectModel(int cLecturerCourseSubjectId, int cLecturerId, int cCourseSubjectId, DateTime cStartDate, DateTime cEndDate, int cIsFlagged)
            : base(cCourseSubjectId)
        {
            LCSId = cLecturerCourseSubjectId;
            LecturerId = cLecturerId;
            StartDate = cStartDate;
            EndDate = cEndDate;
            IsFlagged = cIsFlagged;
        }

        public int IsFlagged
        {
            get { return isFlagged; }
            set { isFlagged = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public int LecturerId
        {
            get { return lecturerId; }
            set { lecturerId = value; }
        }

        public int LCSId
        {
            get { return lcsId; }
            set { lcsId = value; }
        }
        #endregion

        #region CRUD
        public bool AddLecturerCourseSubject()
        {
            bool isInserted = false;
            string tableName = "LecturerCourseSubject";
            List<string> columns = new List<string>(), colValues = ConvertToColumnValues(this);
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            columns.Add("LecturerID");
            columns.Add("CourseSubjectID");
            columns.Add("startDate");
            columns.Add("endDate");
            columns.Add("isFlagged");

            isInserted = dh.Insert(tableName, columns, colValues);
            columns.Clear();
            colValues.Clear();

            return isInserted;
        }

        public bool ChangeLecturerCourseSubject(List<string> where)
        {
            bool isUpdated = false;
            List<string> colValues = ConvertToColumnValues(this);
            Dictionary<string, string> colValuePairs = new Dictionary<string, string>();
            string tableName = "LecturerCourseSubject";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();

            colValuePairs.Add("LecturerID", colValues[0]);
            colValuePairs.Add("CourseSubjectID", colValues[1]);
            colValuePairs.Add("startDate", colValues[2]);
            colValuePairs.Add("endDate", colValues[3]);
            colValuePairs.Add("isFlagged", colValues[4]);
            isUpdated = dh.Update(tableName, colValuePairs, where);

            return isUpdated;
        }

        public bool RemoveLecturerCourseSubject(List<string> where)
        {
            bool isDeleted = false;
            string tableName = "LecturerCourseSubject";
            DataAccess.DataHandler dh = new DataAccess.DataHandler();
            isDeleted = dh.Delete(tableName, where);

            return isDeleted;
        }

        public List<LecturerCourseSubjectModel> GetLecturerCourseSubject(List<string> columns = null, List<string> where = null)
        {
            string tableName = "LecturerCourseSubject";
            List<LecturerCourseSubjectModel> lecturerCourseSubjects = new List<LecturerCourseSubjectModel>();
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
                            fieldValues.Add("LecturerID", attribute);
                            break;
                        case 3:
                            fieldValues.Add("CourseSubjectID", attribute);
                            break;
                        case 4:
                            fieldValues.Add("startDate", attribute);
                            break;
                        case 5:
                            fieldValues.Add("endDate", attribute);
                            break;
                        case 6:
                            fieldValues.Add("isFlagged", attribute);
                            break;
                    }
                    counter++;
                }
                lecturerCourseSubjects.Add(ConvertToLecturerCourseSubject(fieldValues));
            }
            return lecturerCourseSubjects;
        }
        #endregion

        #region Data Manipulation
        private List<string> ConvertToColumnValues(LecturerCourseSubjectModel lcs)
        {
            List<string> colValues = new List<string>();

            try
            {
                string CourseStartDate = "", CourseEndDate = "";
                CourseStartDate = CourseStartDate + lcs.StartDate.Year;
                if (lcs.StartDate.Month < 10)
                    CourseStartDate = CourseStartDate + "0" + lcs.StartDate.Month;
                else
                    CourseStartDate = CourseStartDate + lcs.StartDate.Month;
                if (lcs.StartDate.Day < 10)
                    CourseStartDate = CourseStartDate + "0" + lcs.StartDate.Day;
                else
                    CourseStartDate = CourseStartDate + lcs.StartDate.Day;

                CourseEndDate = CourseEndDate + lcs.EndDate.Year;
                if (lcs.EndDate.Month < 10)
                    CourseEndDate = CourseEndDate + "0" + lcs.EndDate.Month;
                else
                    CourseEndDate = CourseEndDate + lcs.EndDate.Month;
                if (lcs.EndDate.Day < 10)
                    CourseEndDate = CourseEndDate + "0" + lcs.EndDate.Day;
                else
                    CourseEndDate = CourseEndDate + lcs.EndDate.Day;

                colValues.Add(lcs.CourseSubjectId.ToString());
                colValues.Add(lcs.LecturerId.ToString());
                colValues.Add("'" + CourseStartDate + "'");
                colValues.Add("'" + CourseEndDate + "'");
                colValues.Add(lcs.IsFlagged.ToString());
            }
            catch (Exception)
            {
                throw;
            }

            return colValues;
        }

        private static LecturerCourseSubjectModel ConvertToLecturerCourseSubject(Dictionary<string, string> fieldValues)
        {
            LecturerCourseSubjectModel lcs = new LecturerCourseSubjectModel();
            foreach (KeyValuePair<string, string> row in fieldValues)
            {
                switch (row.Key)
                {
                    case "id":
                        try
                        {
                            lcs.LCSId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "CourseSubjectID":
                        try
                        {
                            lcs.CourseSubjectId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "LecturerID":
                        try
                        {
                            lcs.LecturerId = int.Parse(row.Value);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case "startDate":
                        string[] dateParts = row.Value.Split('/');
                        lcs.StartDate = new DateTime(int.Parse(dateParts[0]), int.Parse(dateParts[1]), int.Parse(dateParts[2].Substring(0, 2)));
                        break;
                    case "endDate":
                        dateParts = row.Value.Split('/');
                        lcs.EndDate = new DateTime(int.Parse(dateParts[0]), int.Parse(dateParts[1]), int.Parse(dateParts[2].Substring(0, 2)));
                        break;
                    case "isFlagged":
                        try
                        {
                            lcs.IsFlagged = int.Parse(row.Value);
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
            return lcs;
        }
        #endregion
    }
}