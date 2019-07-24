using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP321MVC.Models
{
    public class QuestionsSelectionModel
    {
        public int NumberOfQuestions { get; set; }
        public OutcomeModel outcome { get; set; }
        public OutcomeDetailsModel outcomeDetails { get; set; }
        public QuestionTypeModel questionType { get; set; } = new QuestionTypeModel();
        public List<OutcomeModel> outcomesList { get; set; }
        public List<QuestionTypeModel> questionTypesList { get; set; } = new List<QuestionTypeModel>();
        public List<OutcomeDetailsModel> outcomeDetailsList { get; set; }
        public List<OutcomeDetailsModel> outcomeDetailsListRelavent { get; set; } = new List<OutcomeDetailsModel>();
        public QuestionModel question { get; set; } = new QuestionModel();
        public List<QuestionModel> questionsList { get; set; }
        public List<QuestionModel> questionListRelavent { get; set; } = new List<QuestionModel>();
        public string[] TypesOfQuestion { get; set; }
        public bool isSelected { get; set; }
        public List<SubjectModel> subjects { get; set; }

        public List<QuestionsSelectionModel> QuestionsSelectionModelsList { get; set; }

        public QuestionsSelectionModel(string Subject)
        {
            subjects = SubjectModel.GetSubject(null, new List<string> { " name = '" + Subject + "'" });
            outcomesList = OutcomeModel.GetOutcome(null, new List<string> { "SubjectID = " + subjects[0].SubjectId });
            outcomeDetailsList = OutcomeDetailsModel.GetOutcomeDetails(null, null);
            foreach (OutcomeModel item in outcomesList)
            {
                foreach (OutcomeDetailsModel item2 in outcomeDetailsList)
                {
                    if (item.OutcomeId == item2.OutcomeId)
                    {
                        outcomeDetailsListRelavent.Add(item2);
                    }
                }
            }

            questionTypesList = QuestionTypeModel.GetQuestionType(null, null);
            questionsList = question.GetQuestion(null, null);

            foreach (QuestionModel item in questionsList)
            {
                foreach (OutcomeDetailsModel item2 in outcomeDetailsListRelavent)
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