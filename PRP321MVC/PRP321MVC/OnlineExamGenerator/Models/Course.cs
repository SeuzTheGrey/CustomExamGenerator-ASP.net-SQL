using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class Course
    {

        private int id;
        private string courseCode;
        private string courseName;
        private int nqfLevel;
        private string courseDescription;

        #region Properties & Constructors
        public Course()
        {
            CourseId = 0;
            CourseCode = "";
            CourseName = "";
            NQFLevel = 0;
            CourseDescription = "";
        }

        public Course(int cCourseId)
        {
            CourseId = cCourseId;
            CourseCode = "";
            CourseName = "";
            NQFLevel = 0;
            CourseDescription = "";
        }

        public Course(string cCourseCode, string cCourseName, int cNQFLevel, string cCourseDescription)
        {
            CourseId = 0;
            CourseCode = cCourseCode;
            CourseName = cCourseName;
            NQFLevel = cNQFLevel;
            CourseDescription = cCourseDescription;
        }

        internal Course(int cCourseId, string cCourseCode, string cCourseName, int cNQFLevel, string cCourseDescription)
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

    }
}