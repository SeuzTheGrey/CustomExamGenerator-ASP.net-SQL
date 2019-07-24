using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class QuestionType
    {
        private int questionTypeId;
        private string name;
        private string type;

        #region Properties & Constructors
        public QuestionType()
        {
            QuestionTypeId = 0;
            Name = "";
            Type = "";
        }

        public QuestionType(int cQuestionTypeId)
        {
            QuestionTypeId = cQuestionTypeId;
            Name = "";
            Type = "";
        }

        public QuestionType(string cName, string cType)
        {
            QuestionTypeId = 0;
            Name = cName;
            Type = cType;
        }

        internal QuestionType(int cQuestionTypeId, string cName, string cType)
        {
            QuestionTypeId = cQuestionTypeId;
            Name = cName;
            Type = cType;
        }

        public int QuestionTypeId
        {
            get { return questionTypeId; }
            set { questionTypeId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion
    }
}