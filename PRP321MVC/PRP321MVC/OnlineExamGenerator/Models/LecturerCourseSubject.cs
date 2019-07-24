using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class LecturerCourseSubject : CourseSubject
    {

        private int lcsId;
        private int lecturerId;
        private DateTime startDate;
        private DateTime endDate;
        private int isFlagged;

        #region Properties & Constructors
        public LecturerCourseSubject()
            : base()
        {
            LCSId = 0;
            LecturerId = 0;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public LecturerCourseSubject(int cLecturerCourseSubjectId)
            : base()
        {
            LCSId = cLecturerCourseSubjectId;
            LecturerId = 0;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public LecturerCourseSubject(int cLecturerId, int cCourseSubjectId, DateTime cStartDate, DateTime cEndDate, int cIsFlagged)
            : base(cCourseSubjectId)
        {
            LCSId = 0;
            LecturerId = cLecturerId;
            StartDate = cStartDate;
            EndDate = cEndDate;
            IsFlagged = cIsFlagged;
        }

        internal LecturerCourseSubject(int cLecturerCourseSubjectId, int cLecturerId, int cCourseSubjectId, DateTime cStartDate, DateTime cEndDate, int cIsFlagged)
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

    }
}