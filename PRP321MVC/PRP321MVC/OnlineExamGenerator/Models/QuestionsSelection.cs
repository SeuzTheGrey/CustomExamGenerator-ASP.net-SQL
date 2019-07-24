using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class QuestionsSelection
    {
        public int NumberOfQuestions { get; set; }
        public Outcome outcome { get; set; }
        public OutcomeDetails outcomeDetails { get; set; }
        public QuestionType questionType { get; set; } = new QuestionType();
        public List<Outcome> outcomesList { get; set; }
        public List<QuestionType> questionTypesList { get; set; } = new List<QuestionType>();
        public List<OutcomeDetails> outcomeDetailsList { get; set; }
        public List<OutcomeDetails> outcomeDetailsListRelavent { get; set; } = new List<OutcomeDetails>();
        public Question question { get; set; } = new Question();
        public List<Question> questionsList { get; set; }
        public List<Question> questionListRelavent { get; set; } = new List<Question>();
        public string[] TypesOfQuestion { get; set; }
        public bool isSelected { get; set; }
        public List<Subject> subjects { get; set; }

        public List<QuestionsSelection> QuestionsSelectionModelsList { get; set; }

        public QuestionsSelection(string Subject)
        {
            subjects = Subject.GetSubject(null, new List<string> { " name = '" + Subject + "'" });
            outcomesList = Outcome.GetOutcome(null, new List<string> { "SubjectID = " + subjects[0].SubjectId });
            outcomeDetailsList = OutcomeDetails.GetOutcomeDetails(null, null);
            foreach (Outcome item in outcomesList)
            {
                foreach (OutcomeDetails item2 in outcomeDetailsList)
                {
                    if (item.OutcomeId == item2.OutcomeId)
                    {
                        outcomeDetailsListRelavent.Add(item2);
                    }
                }
            }

            questionTypesList = QuestionType.GetQuestionType(null, null);
            questionsList = question.GetQuestion(null, null);

            foreach (Question item in questionsList)
            {
                foreach (OutcomeDetails item2 in outcomeDetailsListRelavent)
                {
                    if (item.OutcomeDetailsId == item2.OutcomeDetailsId)
                    {
                        questionListRelavent.Add(item);
                    }
                }
            }
        }
    }
}