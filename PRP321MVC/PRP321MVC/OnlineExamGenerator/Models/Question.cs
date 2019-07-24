using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class Question : OutcomeDetails
    {
        private int questionId;
        private int questionTypeId;
        private string questions;
        private int weight;

        #region Properties & Constructors
        public Question()
            : base()
        {
            QuestionId = 0;
            QuestionTypeId = 0;
            Questions = "";
            Weight = 0;
        }

        public Question(int cQuestionId)
            : base()
        {
            QuestionId = cQuestionId;
            QuestionTypeId = 0;
            Questions = "";
            Weight = 0;
        }

        internal Question(int cQuestionId, int cOutcomeDetailsId, int cQuestionTypeId, string cQuestion, int cWeight)
            : base(cOutcomeDetailsId)
        {
            QuestionId = cQuestionId;
            QuestionTypeId = cQuestionTypeId;
            Questions = cQuestion;
            Weight = cWeight;
        }

        public Question(int cOutcomeDetailsId, int cQuestionTypeId, string cQuestion, int cWeight, string cOutcomeName, string cCode)
            : base(cOutcomeDetailsId)
        {
            QuestionTypeId = cQuestionTypeId;
            Questions = cQuestion;
            Weight = cWeight;
            base.OutcomeName = cOutcomeName;
            base.Code = cCode;
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string Questions
        {
            get { return questions; }
            set { questions = value; }
        }

        public int QuestionTypeId
        {
            get { return questionTypeId; }
            set { questionTypeId = value; }
        }

        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; }
        }
        #endregion
    }
}