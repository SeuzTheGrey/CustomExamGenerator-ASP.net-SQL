using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class QuestionChoices : Question
    {

        // still thinking on this class

        private int id;
        private string choice;

        #region Properties & Constructors
        public QuestionChoices()
            : base()
        {
            ChoiceId = 0;
            Choice = "";
        }

        public QuestionChoices(int cChoiceId)
            : base()
        {
            ChoiceId = cChoiceId;
            Choice = "";
        }

        public QuestionChoices(int cQuestionId, string cChoice)
            : base(cQuestionId)
        {
            ChoiceId = 0;
            Choice = cChoice;
        }

        internal QuestionChoices(int cChoiceId, int cQuestionId, string cChoice)
            : base(cQuestionId)
        {
            ChoiceId = cChoiceId;
            Choice = cChoice;
        }

        public QuestionChoices(string cChoice, int cOutcomeDetailsId, int cQuestionTypeId, string cQuestion, int cWeight, string cOutcomeName, string cCode)
            : base(cOutcomeDetailsId, cQuestionTypeId, cQuestion, cWeight, cOutcomeName, cCode)
        {
            Choice = cChoice;
        }

        public string Choice
        {
            get { return choice; }
            set { choice = value; }
        }

        public int ChoiceId
        {
            get { return id; }
            set { id = value; }
        }
        #endregion 
    }
}