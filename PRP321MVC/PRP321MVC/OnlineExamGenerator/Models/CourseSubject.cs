using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class CourseSubject : Subject
    {
        private int courseSubjectId;
        private int courseId;

        #region Properties & Constructors
        public CourseSubject()
            : base()
        {
            CourseSubjectId = 0;
            CourseId = 0;
        }

        public CourseSubject(int cCourseSubjectId)
            : base()
        {
            CourseSubjectId = cCourseSubjectId;
            CourseId = 0;
        }

        public CourseSubject(int cSubjectId, int cCourseId)
            : base(cSubjectId)
        {
            CourseSubjectId = 0;
            CourseId = cCourseId;
        }

        internal CourseSubject(int cCourseSubjectId, int cSubjectId, int cCourseId)
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
    
    }
}