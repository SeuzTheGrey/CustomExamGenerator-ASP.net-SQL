using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class OutcomesCollectionModel
    {
        public int QuestionsNumber { get; set; }
        public string QuestionType { get; set; }
        public List<string> OutcomesList { get; set; }

        public OutcomesCollectionModel()
        {

        }

        public OutcomesCollectionModel(int cQuestionsNumber, string cQuestionType, List<string> cOutcomesList)
        {
            QuestionsNumber = cQuestionsNumber;
            QuestionType = cQuestionType;
            OutcomesList = cOutcomesList;
        }

        
    }
}