using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class PracticalWeight
    {

        private int id;
        private int quesionId;
        private int questionWeight;
        private string questionBreakdown;

        public PracticalWeight()
        {

        }

        public PracticalWeight(int cId, int cQuestionId, int cQuestionWeight, string cQuestionBreakdown)
        {
            id = cId;
            quesionId = cQuestionId;
            questionWeight = cQuestionWeight;
            questionBreakdown = cQuestionBreakdown;
        }

        public string QuestionBreakdown
        {
            get { return questionBreakdown; }
            set { questionBreakdown = value; }
        }

        public int QuestionWeight
        {
            get { return questionWeight; }
            set { questionWeight = value; }
        }

        public int QuestionId
        {
            get { return quesionId; }
            set { quesionId = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}