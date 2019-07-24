using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class Outcome : Subject
    {
        private int id;
        private string outcomeName;

        #region Properties & Constructors
        public Outcome()
            : base()
        {
            OutcomeId = 0;
            OutcomeName = "";
        }

        public Outcome(int cOutcomeId)
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

        internal Outcome(int cOutcomeId, int cSubjectId, string cOutcomeName)
            : base(cSubjectId)
        {
            OutcomeId = cOutcomeId;
            OutcomeName = cOutcomeName;
        }

        public Outcome(string cOutcomeName, string cCode)
            : base(cCode)
        {
            OutcomeId = 0;
            OutcomeName = cOutcomeName;
            Code = cCode;
        }

        public Outcome(int cId, string cOutcomeName)
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
    }
}