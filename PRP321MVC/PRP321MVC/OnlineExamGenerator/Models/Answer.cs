using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class Answer
    {
        private int id;
        private int questionId;
        private string answer;

        #region Properties & Constructors
        public Answer()
        {
            Id = 0;
            QuestionId = 0;
            Answers = "";
        }

        public Answer(int cId)
        {
            Id = cId;
            QuestionId = 0;
            Answers = "";
        }

        internal Answer(int cId, int cQuestionId, string cAnswer)
        {
            Id = cId;
            QuestionId = cQuestionId;
            Answers = cAnswer;
        }

        public string Answers
        {
            get { return answer; }
            set { answer = value; }
        }

        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
    }
}