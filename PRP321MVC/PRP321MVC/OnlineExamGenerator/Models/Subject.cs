using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class Subject
    {

        private int id;
        private string name;
        private string code;
        private int duration;
        private int weight;
        private string description;

        #region Properties & Constructors
        public Subject()
        {
            SubjectId = 0;
            Name = "";
            Code = "";
            Duration = 0;
            Weight = 0;
            Description = "";
        }

        public Subject(int cId)
        {
            SubjectId = cId;
            Name = "";
            Code = "";
            Duration = 0;
            Weight = 0;
            Description = "";
        }

        public Subject(string cName, string cCode, int cDuration, int cWeight, string cDescription)
        {
            SubjectId = 0;
            Name = cName;
            Code = cCode;
            Duration = cDuration;
            Weight = cWeight;
            Description = cDescription;
        }

        internal Subject(int cId, string cName, string cCode, int cDuration, int cWeight, string cDescription)
        {
            SubjectId = cId;
            Name = cName;
            Code = cCode;
            Duration = cDuration;
            Weight = cWeight;
            Description = cDescription;
        }

        public Subject(string cCode)
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
    }
}