using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OnlineExamGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class Generator
    {

        enum Letters { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z }

        public class GeneratorModel
        {
            public Random Random { get; set; } = new Random();
            public string lecturerName { get; set; }
            readonly string templeteLocation = "E:\\Third Year Project\\PRP321MVC\\PRP321MVC\\Templates\\Template.docx";
            string outputLocation;
            public Subject Subject { get; set; }
            public string TestType { get; set; }
            public int NumberOfQuestions { get; set; }
            public string Examinator { get; set; }
            public string Moderator { get; set; }
            public string[] TypesOfQuestions { get; set; }
            public List<OutcomesCollection> outcomesForWhere { get; set; }
            public int[] NumberOfQuestionsPerQuestion { get; set; }
            public Dictionary<int, List<Question>> QuestionsChosen { get; set; }
            public string[] QuestionType { get; set; }

            public void CreateExam()
            {
                string examName = lecturerName + Random.Next() + DateTime.UtcNow.ToLongDateString() + ".docx";
                outputLocation = "E:\\Third Year Project\\PRP321MVC\\PRP321MVC\\Exams\\" + lecturerName + "\\" + examName;
                File.Copy(templeteLocation, outputLocation);
                QuestionsChosen = new Dictionary<int, List<Question>>();
                QuestionType = new string[100];
                for (int i = 0; i < NumberOfQuestions; i++)
                {

                    SearchAndReplace("{LecExaminer}", Examinator);
                    SearchAndReplace("{LecModerator}", Moderator);
                    //SearchAndReplace("", TestType);
                    SearchAndReplace("{SubjectNameCode}", Subject.Name);

                    if (TypesOfQuestions[i] == "MultipleChoice")
                    {
                        CreateMultipleChoice(NumberOfQuestionsPerQuestion[i], i + 1, outcomesForWhere[i], i);
                        QuestionType[i] = "Multiple Choice";

                    }
                    else if (TypesOfQuestions[i] == "MatchTheColumns")
                    {
                        MatchColumnsQuestions(NumberOfQuestionsPerQuestion[i], i + 1, outcomesForWhere[i], i);
                        QuestionType[i] = "Match the columns";
                    }
                    else if (TypesOfQuestions[i] == "TrueOrFalse")
                    {
                        TrueAndFalseQuestion(NumberOfQuestionsPerQuestion[i], i + 1, outcomesForWhere[i], i);
                        QuestionType[i] = "True Or False";
                    }
                    else if (TypesOfQuestions[i] == "Theory")
                    {
                        TheoryQuestions(NumberOfQuestionsPerQuestion[i], i + 1, outcomesForWhere[i], i);
                        QuestionType[i] = "Theory";
                    }
                    else if (TypesOfQuestions[i] == "Practical")
                    {
                        PracticalQuestions(NumberOfQuestionsPerQuestion[i], i + 1, outcomesForWhere[i], i);
                        QuestionType[i] = "Practical";
                    }


                }

                MemoGenerator memo = new MemoGenerator(QuestionsChosen, QuestionType, examName);
                memo.CreateMemo();
            }

            /// <summary>
            /// This to Replace a single word in the template. be careful with what words you wish to change it could cause corruption of the file
            /// </summary>
            /// <param name="word">the word you want replaced</param>
            /// <param name="replacementWord">the word you want insted</param>
            public void SearchAndReplace(string word, string replacementWord)
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputLocation, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    Regex regexText = new Regex(word);
                    docText = regexText.Replace(docText, replacementWord);

                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }
            }

            /// <summary>
            /// This Method is for multiple choice of the exam paper
            /// </summary>
            /// <param name="NumberOfQuestions">This is the number of questions under the multiple choice section</param>
            /// <param name="QuestionNumber">the number of the question in the exam</param>
            public void CreateMultipleChoice(int NumberOfQuestions, int QuestionNumber, OutcomesCollection outcomesWhere, int ArrayPosition)
            {
                int Counter = 1;
                int Total = NumberOfQuestions * 2;
                string[] QuestionsUsed = new string[NumberOfQuestions]; // i cant remember what i wanted here
                Random random = new Random();
                Question question = new Question();
                QuestionChoices questionChoice = new QuestionChoices();
                Question ChoosenQuestion = new Question();
                List<Question> QuestionsChoosenForDi = new List<Question>();




                //Dont forget to include a where clause for the questions and choices for outcomes chosen and types for the questions

                //List<Question> questionList = question.Select(outcomesWhere.OutcomesList);
                //foreach (Question item in questionList)
                //{
                //    List<string> where = new List<string>() { string.Format("id = {0}", item.QuestionId)  }; //question id for the where clause

                //    List<QuestionChoice> questionChoiceList = questionChoice.GetQuestionChoice(null, where); 
                //}


                using (WordprocessingDocument doc = WordprocessingDocument.Open(outputLocation, true))
                {
                    Body docBody = doc.MainDocumentPart.Document.Body;

                    Table table = new Table();

                    TableRow row1 = new TableRow();
                    TableRow row2 = new TableRow();

                    TableCell cell = new TableCell();
                    cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                    TableCell cell2 = new TableCell();

                    cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));

                    TableCell cell3 = new TableCell();
                    cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "500" }));

                    Paragraph paragraph = new Paragraph(new Run(new Text(string.Format("Question {0}", QuestionNumber))));
                    Paragraph paragraph1 = new Paragraph(new Run(new Text(string.Format("[{0}]", Total))));

                    cell.Append(paragraph);
                    cell3.Append(paragraph1);
                    row1.Append(cell);
                    row1.Append(cell2);
                    row1.Append(cell3);

                    table.Append(row1);

                    // this loop is for the creation of a question and the choices for the question
                    for (int i = 0; i < NumberOfQuestions; i++)
                    {
                        TableRow tableRow = new TableRow();
                        cell = new TableCell();
                        cell2 = new TableCell();
                        cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell3 = new TableCell();
                        cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                        //ChoosenQuestion = questionList[random.Next(questionList.Count)];
                        //questionList.Remove(ChoosenQuestion);

                        Paragraph Question = new Paragraph(new Run(new Text(Counter + "." + ChoosenQuestion.Questions)));
                        Paragraph QuestionWeight = new Paragraph(new Run(new Text(ChoosenQuestion.Weight.ToString())));

                        //QuestionsChosen[ArrayPosition].Add(ChoosenQuestion);
                        QuestionsChoosenForDi.Add(ChoosenQuestion);


                        cell.Append(Question);
                        cell3.Append(QuestionWeight);

                        tableRow.Append(cell);
                        tableRow.Append(cell2);
                        tableRow.Append(cell3);

                        table.Append(tableRow);

                        TableRow tableRow2 = new TableRow();

                        List<string> where = new List<string>() { string.Format("QuestionID = {0}", ChoosenQuestion.QuestionId) };

                        //List<QuestionChoices> questionChoiceList = questionChoice.GetQuestionChoice(null, where);

                        // this to select a letter according to the number of the choice
                        int LetterCounter = 0;
                        Letters letters = (Letters)LetterCounter;
                        //this will write out the choices for a question
                        //foreach (QuestionChoices item in questionChoiceList)
                        //{
                        //    cell = new TableCell();
                        //    cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "500" }));
                        //    cell2 = new TableCell();
                        //    tableRow2 = new TableRow();
                        //    Paragraph Choices = new Paragraph(new Run(new Text("    " + letters.ToString() + "." + item.Choice)));
                        //    LetterCounter++;
                        //    cell2.Append(Choices);
                        //    tableRow2.Append(cell);
                        //    tableRow2.Append(cell2);
                        //    table.Append(tableRow2);
                        //    letters = (Letters)LetterCounter;
                        //}

                        Counter++;
                    }

                    docBody.Append(table);
                    QuestionsChosen.Add(ArrayPosition, QuestionsChoosenForDi);
                }
            }


            /// <summary>
            /// This Method is for Theory Questions of the exam paper
            /// </summary>
            /// <param name="NumberOfQuestions">This is the number of questions under the multiple choice section</param>
            /// <param name="QuestionNumber">the number of the question in the exam</param>
            public void TheoryQuestions(int NumberOfQuestions, int QuestionNumber, OutcomesCollection outcomesWhere, int ArrayPosition)
            {


                Random random = new Random();
                Question question = new Question();
                Question ChoosenQuestion = new Question();
                List<Question> QuestionsChoosenForDi = new List<Question>();


                //Dont forget to include a where clause for the questions and choices for outcomes chosen and types for the questions

               // List<Question> questionList = question.Select(outcomesWhere.OutcomesList);

                using (WordprocessingDocument doc = WordprocessingDocument.Open(outputLocation, true))
                {
                    Body docBody = doc.MainDocumentPart.Document.Body;

                    Table table = new Table();

                    TableRow row1 = new TableRow();
                    TableRow row2 = new TableRow();

                    TableCell cell = new TableCell();
                    cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                    TableCell cell2 = new TableCell();

                    cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));

                    TableCell cell3 = new TableCell();
                    cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "500" }));

                    Paragraph paragraph = new Paragraph(new Run(new Text(string.Format("Question {0}", QuestionNumber))));
                    Paragraph paragraph1 = new Paragraph(new Run(new Text("Weight")));

                    cell.Append(paragraph);
                    cell3.Append(paragraph1);
                    row1.Append(cell);
                    row1.Append(cell2);
                    row1.Append(cell3);
                    table.Append(row1);

                    int LetterCounter = 0;
                    Letters letters = (Letters)LetterCounter;

                    for (int i = 0; i < NumberOfQuestions; i++)
                    {
                        TableRow tableRow = new TableRow();
                        cell = new TableCell();
                        cell2 = new TableCell();
                        cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell3 = new TableCell();
                        cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                        //ChoosenQuestion = questionList[random.Next(questionList.Count)];
                        //questionList.Remove(ChoosenQuestion);

                        //QuestionsChosen[ArrayPosition].Add(ChoosenQuestion);
                        QuestionsChoosenForDi.Add(ChoosenQuestion);

                        Paragraph Question = new Paragraph(new Run(new Text(letters + "." + ChoosenQuestion.Questions)));
                        Paragraph QuestionWeight = new Paragraph(new Run(new Text(ChoosenQuestion.Weight.ToString())));

                        cell.Append(Question);
                        cell3.Append(QuestionWeight);
                        tableRow.Append(cell);
                        tableRow.Append(cell2);
                        tableRow.Append(cell3);
                        table.Append(tableRow);

                        letters = (Letters)LetterCounter;
                    }

                    docBody.Append(table);
                    QuestionsChosen.Add(ArrayPosition, QuestionsChoosenForDi);
                }


            }

            /// <summary>
            /// This Method is for Match the Columns of the exam paper
            /// </summary>
            /// <param name="NumberOfQuestions">This is the number of questions under the multiple choice section</param>
            /// <param name="QuestionNumber">the number of the question in the exam</param>
            public void MatchColumnsQuestions(int NumberOfQuestions, int QuestionNumber, OutcomesCollection outcomesWhere, int ArrayPosition)
            {
                Random random = new Random();
                Question question = new Question();
                Question ChoosenQuestion = new Question();
                OutcomeDetails outcomeDetails = new OutcomeDetails();
                Answer ChosenAnswer = new Answer();
                List<Question> questionsList = new List<Question>();
                List<Answer> answersList = new List<Answer>();

                List<string> answerWhere = new List<string>();
                List<Question> QuestionsChoosenForDi = new List<Question>();
                //Dont forget to include a where clause for the questions and choices for outcomes chosen and types for the questions

               // questionsList = question.Select(outcomesWhere.OutcomesList);

                //fix This later.....
                foreach (Question item in questionsList)
                {
                    answerWhere.Add(item.QuestionId.ToString());
                }
                //answersList = Answer.Select(answerWhere);

                Answer[] ChoosenAnswersArray = new Answer[NumberOfQuestions];
                int[] AnswersUsed = new int[NumberOfQuestions];
                int[] QuestionsUsed = new int[NumberOfQuestions];

                Question[] questionArray = new Question[NumberOfQuestions];
                int Total = NumberOfQuestions * 2;

                using (WordprocessingDocument doc = WordprocessingDocument.Open(outputLocation, true))
                {
                    Body docBody = doc.MainDocumentPart.Document.Body;

                    Table table = new Table();

                    TableRow row1 = new TableRow();
                    TableRow row2 = new TableRow();

                    TableCell cell = new TableCell();
                    cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                    TableCell cell2 = new TableCell();

                    cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));

                    TableCell cell3 = new TableCell();
                    cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "500" }));

                    Paragraph paragraph = new Paragraph(new Run(new Text(string.Format("Question {0}", QuestionNumber))));
                    Paragraph paragraph1 = new Paragraph(new Run(new Text(string.Format("[{0}]", Total))));

                    cell.Append(paragraph);
                    cell3.Append(paragraph1);
                    row1.Append(cell);
                    row1.Append(cell2);
                    row1.Append(cell3);

                    //row1.Append(cell2);

                    int LetterCounter = 0;
                    Letters letters = (Letters)LetterCounter;

                    for (int i = 0; i < NumberOfQuestions; i++)
                    {
                        questionArray[i] = questionsList[random.Next(questionsList.Count)];
                        ChoosenQuestion = questionArray[i];
                        questionsList.Remove(ChoosenQuestion);
                        //QuestionsChosen[ArrayPosition].Add(ChoosenQuestion);
                        QuestionsChoosenForDi.Add(ChoosenQuestion);

                        foreach (Answer item in answersList)
                        {
                            if (ChoosenQuestion.QuestionId == item.QuestionId)
                            {
                                ChoosenAnswersArray[i] = item;
                            }
                        }
                    }

                    for (int i = 0; i < NumberOfQuestions; i++)
                    {
                        TableRow QuestionRow = new TableRow();
                        cell = new TableCell();
                        cell2 = new TableCell();
                        cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell3 = new TableCell();
                        cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                        // this doesnt make sense and needs thought
                        //someone please check this logic



                        for (int j = 0; j < NumberOfQuestions; j++)
                        {
                            int questionRandom = random.Next(questionArray.Length);
                            if (QuestionsUsed[j] != questionRandom)
                            {
                                ChoosenQuestion = questionArray[questionRandom];
                                QuestionsUsed[i] = questionRandom;
                            }
                        }



                        for (int j = 0; j < NumberOfQuestions; j++)
                        {
                            int answerRandom = random.Next(questionArray.Length);
                            if (AnswersUsed[j] != answerRandom)
                            {
                                ChosenAnswer = ChoosenAnswersArray[answerRandom];
                                AnswersUsed[i] = answerRandom;
                            }
                        }
                        int LetterCounter2 = LetterCounter + 1;
                        Paragraph Question = new Paragraph(new Run(new Text(LetterCounter2 + "." + ChoosenQuestion.Questions)));
                        Paragraph Answer = new Paragraph(new Run(new Text(letters + "." + ChosenAnswer.Answers)));
                        Paragraph QuestionWeight = new Paragraph(new Run(new Text(ChoosenQuestion.Weight.ToString())));

                        cell.Append(Question);
                        cell2.Append(Answer);
                        cell3.Append(QuestionWeight);
                        QuestionRow.Append(cell);
                        QuestionRow.Append(cell2);
                        QuestionRow.Append(cell3);
                        table.Append(QuestionRow);
                        letters++;
                        letters = (Letters)LetterCounter;


                    }

                    docBody.Append(table);
                    QuestionsChosen.Add(ArrayPosition, QuestionsChoosenForDi);
                }





            }

            /// <summary>
            /// This Method is for True and false questions of the exam paper
            /// </summary>
            /// <param name="NumberOfQuestions">This is the number of questions under the multiple choice section</param>
            /// <param name="QuestionNumber">the number of the question in the exam</param>
            public void TrueAndFalseQuestion(int NumberOfQuestions, int QuestionNumber, OutcomesCollection outcomesWhere, int ArrayPosition)
            {
                Random random = new Random();
                Question question = new Question();
                Question ChoosenQuestion = new Question();
                List<Question> QuestionsChoosenForDi = new List<Question>();


                //Dont forget to include a where clause for the questions and choices for outcomes chosen and types for the questions

               // List<Question> questionList = question.Select(outcomesWhere.OutcomesList);

                int Total = NumberOfQuestions * 2;

                using (WordprocessingDocument doc = WordprocessingDocument.Open(outputLocation, true))
                {
                    Body docBody = doc.MainDocumentPart.Document.Body;

                    Table table = new Table();

                    TableRow row1 = new TableRow();
                    TableRow row2 = new TableRow();

                    TableCell cell = new TableCell();
                    cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                    TableCell cell2 = new TableCell();

                    cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));

                    TableCell cell3 = new TableCell();
                    cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "500" }));

                    Paragraph paragraph = new Paragraph(new Run(new Text(string.Format("Question {0}", QuestionNumber))));
                    Paragraph paragraph1 = new Paragraph(new Run(new Text(string.Format("[{0}]", Total))));

                    cell.Append(paragraph);
                    cell3.Append(paragraph1);
                    row1.Append(cell);
                    row1.Append(cell2);
                    row1.Append(cell3);

                    table.Append(row1);

                    int LetterCounter = 0;
                    Letters letters = (Letters)LetterCounter;

                    for (int i = 0; i < NumberOfQuestions; i++)
                    {
                        TableRow tableRow = new TableRow();
                        cell = new TableCell();
                        cell2 = new TableCell();
                        cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell3 = new TableCell();
                        cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                        //ChoosenQuestion = questionList[random.Next(questionList.Count)];
                        //questionList.Remove(ChoosenQuestion);
                        //QuestionsChosen[ArrayPosition].Add(ChoosenQuestion);
                        QuestionsChoosenForDi.Add(ChoosenQuestion);

                        Paragraph Question = new Paragraph(new Run(new Text(letters + "." + ChoosenQuestion.Questions)));
                        Paragraph QuestionWeight = new Paragraph(new Run(new Text(ChoosenQuestion.Weight.ToString())));

                        cell.Append(Question);
                        cell2.Append(new Paragraph(new Run(new Text("True Or False"))));
                        cell3.Append(QuestionWeight);
                        tableRow.Append(cell);
                        tableRow.Append(cell2);
                        tableRow.Append(cell3);
                        table.Append(tableRow);

                        letters = (Letters)LetterCounter;
                    }

                    docBody.Append(table);
                    QuestionsChosen.Add(ArrayPosition, QuestionsChoosenForDi);
                }
            }

            /// <summary>
            /// This Method is for Practical Questions of the exam paper
            /// </summary>
            /// <param name="NumberOfQuestions">This is the number of questions under the multiple choice section</param>
            /// <param name="QuestionNumber">the number of the question in the exam</param>
            public void PracticalQuestions(int NumberOfQuestions, int QuestionNumber, OutcomesCollection outcomesWhere, int ArrayPosition)
            {
                Random random = new Random();
                Question question = new Question();
                Question ChoosenQuestion = new Question();
                List<Question> QuestionsChoosenForDi = new List<Question>();
                //

                List<PracticalWeight> weightsList = new List<PracticalWeight>();



                //Dont forget to include a where clause for the questions and choices for outcomes chosen and types for the questions

                 // List<Question> questionList = question.Select(outcomesWhere.OutcomesList);

                int Total = NumberOfQuestions * 2;

                using (WordprocessingDocument doc = WordprocessingDocument.Open(outputLocation, true))
                {
                    Body docBody = doc.MainDocumentPart.Document.Body;

                    Table table = new Table();

                    TableRow row1 = new TableRow();
                    TableRow row2 = new TableRow();

                    TableCell cell = new TableCell();
                    cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                    TableCell cell2 = new TableCell();

                    cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));

                    TableCell cell3 = new TableCell();
                    cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "500" }));

                    Paragraph paragraph = new Paragraph(new Run(new Text(string.Format("Question {0}", QuestionNumber))));
                    Paragraph paragraph1 = new Paragraph(new Run(new Text(string.Format("[{0}]", Total))));

                    cell.Append(paragraph);
                    cell3.Append(paragraph1);
                    row1.Append(cell);
                    row1.Append(cell2);
                    row1.Append(cell3);

                    int LetterCounter = 0;
                    Letters letters = (Letters)LetterCounter;

                    for (int i = 0; i < NumberOfQuestions; i++)
                    {
                        table = new Table();
                        TableRow tableRow = new TableRow();
                        cell = new TableCell();
                        cell2 = new TableCell();
                        cell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));
                        cell3 = new TableCell();
                        cell3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2500" }));

                        //ChoosenQuestion = questionList[random.Next(questionList.Count)];
                        //questionList.Remove(ChoosenQuestion);
                        //QuestionsChosen[ArrayPosition].Add(ChoosenQuestion);
                        QuestionsChoosenForDi.Add(ChoosenQuestion);
                       // weightsList = PracticalWeight.Select(ChoosenQuestion.QuestionId.ToString());

                        Paragraph Question = new Paragraph(new Run(new Text(letters + "." + ChoosenQuestion.Questions)));
                        Paragraph QuestionWeight = new Paragraph(new Run(new Text(ChoosenQuestion.Weight.ToString())));

                        cell.Append(Question);
                        cell3.Append(QuestionWeight);
                        tableRow.Append(cell);
                        tableRow.Append(cell2);
                        tableRow.Append(cell3);
                        table.Append(tableRow);
                        LetterCounter++;
                        letters = (Letters)LetterCounter;

                        docBody.Append(table);




                        foreach (PracticalWeight item in weightsList)
                        {
                            table = new Table();

                            TableProperties props = new TableProperties();
                            TableStyle tableStyle = new TableStyle { Val = "LightShading-Accent1" };
                            props.TableStyle = tableStyle;
                            table.AppendChild(props);

                            row1 = new TableRow();
                            cell = new TableCell();
                            cell2 = new TableCell();

                            Paragraph Breakdown = new Paragraph(new Run(new Text(item.QuestionBreakdown)));
                            Paragraph Weight = new Paragraph(new Run(new Text(item.QuestionWeight.ToString())));

                            cell.Append(Breakdown);
                            cell2.Append(Weight);

                            row1.Append(cell);
                            row1.Append(cell2);

                            table.Append(row1);
                            docBody.Append(table);
                        }


                    }


                    QuestionsChosen.Add(ArrayPosition, QuestionsChoosenForDi);
                }



            }

        }



    }
    class MemoGenerator
    {
        public readonly string templateLocation = "E:\\Third Year Project\\PRP321MVC\\PRP321MVC\\Templates\\MemoTemplate.docx";
        string outputLocation;
        public string examName { get; set; }
        public Dictionary<int, List<Question>> QuestionsChosen { get; set; }
        public string[] QuestionType { get; set; }

        public MemoGenerator()
        {

        }

        public MemoGenerator(Dictionary<int, List<Question>> QuestionsChosen, string[] QuestionType, string examName)
        {
            this.QuestionsChosen = QuestionsChosen;
            this.QuestionType = QuestionType;
            this.examName = examName;
            outputLocation = "E:\\Third Year Project\\PRP321MVC\\PRP321MVC\\Memos\\" + examName;
            File.Copy(templateLocation, outputLocation);
        }

        public void CreateMemo()
        {

            memoGeneration(QuestionsChosen, QuestionType);

        }

        public void memoGeneration(Dictionary<int, List<Question>> questions, string[] questionType)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(outputLocation, true))
            {

                int counter = 0;

                foreach (KeyValuePair<int, List<Question>> entry in questions)
                {
                    Body docBody = doc.MainDocumentPart.Document.Body;

                    Run run = new Run();
                    RunProperties properties = new RunProperties();
                    properties.Bold = new Bold();
                    run.Append(properties);
                    run.Append(new Text(questionType[counter]));

                    Paragraph QuestionType = new Paragraph(run);

                    docBody.Append(QuestionType);
                    foreach (Question item in entry.Value)
                    {

                        //List<Answer> answers = Answer.GetAnswer(null, new List<string>() { string.Format(" id = {0} ", item.QuestionId) });

                        Paragraph questionTitle = new Paragraph(new Run(new Text("Question")));
                        Paragraph Question = new Paragraph(new Run(new Text(item.Questions)));
                        Paragraph answerTitle = new Paragraph(new Run(new Text("Answer")));
                        //Paragraph Answer = new Paragraph(new Run(new Text(answers[0].Answers)));
                       // answers = new List<Answer>();
                        docBody.Append(questionTitle);
                        docBody.Append(Question);
                        docBody.Append(answerTitle);
                        //docBody.Append(Answer);

                    }
                    counter++;
                }



            }
        }
    }
}

    