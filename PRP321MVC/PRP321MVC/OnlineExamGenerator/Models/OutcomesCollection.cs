using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class OutcomesCollection
    {

        public int QuestionsNumber { get; set; }
        public string QuestionType { get; set; }
        public List<string> OutcomesList { get; set; }

        public OutcomesCollection()
        {

        }

        public OutcomesCollection(int cQuestionsNumber, string cQuestionType, List<string> cOutcomesList)
        {
            QuestionsNumber = cQuestionsNumber;
            QuestionType = cQuestionType;
            OutcomesList = cOutcomesList;
        }

    }
}