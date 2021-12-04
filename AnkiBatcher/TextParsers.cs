using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkiBatcher
{
    class TextParsers
    {
        public static void CallFlashcardCreator(string path, string quiz, List<Question> noDupes)
        {
            switch (Program.flashcardStyle)
            {
                case Program.flashcardStyleEnum.AllMultipleChoice:
                    CreateFlashcards.AllMultipleChoice(path, quiz, noDupes);
                    break;
                case Program.flashcardStyleEnum.MCTFCloze:
                    CreateFlashcards.MCTFCloze(path, quiz, noDupes);
                    break;
                case Program.flashcardStyleEnum.AdvancedCloze:
                    CreateFlashcards.AdvancedCloze(path, quiz, noDupes);
                    break;
                case Program.flashcardStyleEnum.Basic:
                    CreateFlashcards.Basic(path, quiz, noDupes);
                    break;
                case Program.flashcardStyleEnum.Dynamic:
                    CreateFlashcards.Dynamic(path, quiz, noDupes);
                    break;
                default:
                    Console.WriteLine("Error: flashcardStyle not accepted.");
                    break;
            }
        }

        public static void SelfQuizParse(string path, string quiz)
        {
            //Parse File for questions

            string[] lines = System.IO.File.ReadAllLines(quiz);

            int questionsFound = 0;

            List<int> questionIndex = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {

                if (lines[i].Contains("Question text"))
                {
                    //System.Console.WriteLine("found question at index: " + i);
                    questionIndex.Add(i);
                    questionsFound++;
                }
            }

            //parse 1 question

            int QuestionNum = 0;

            var QuestionArray = new List<Question>();


            for (int i = 0; i < lines.Length; i++)
            {
                //Looping through all lines

                var currentLine = lines[i];

                if (currentLine.Contains("Question text"))
                {
                    // TODO: Updated all Lists of Questions to the appropriate question type
                    Question newQuestion = new Question(lines[i + 1], 0, "");

                    QuestionArray.Add(newQuestion);
                    QuestionNum++;
                }

                if (currentLine.Contains("Select one:"))
                {
                    if (lines[i + 2].Contains("a."))
                    {
                        QuestionArray[QuestionNum - 1].QuestionType = 1;
                    }
                    else
                    {
                        QuestionArray[QuestionNum - 1].QuestionType = 0;
                        QuestionArray[QuestionNum - 1].QuestionA = "True";
                        QuestionArray[QuestionNum - 1].QuestionB = "False";
                    }
                }
                else if (currentLine.Contains("Select one or more:"))
                {
                    QuestionArray[QuestionNum - 1].QuestionType = 2;
                }

                if (QuestionArray.Count > 1)
                {
                    if (currentLine.Contains("a.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        QuestionArray[QuestionNum - 1].QuestionA = lines[i + 1];
                    }

                    if (currentLine.Contains("b.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        QuestionArray[QuestionNum - 1].QuestionB = lines[i + 1];

                    }

                    if (currentLine.Contains("c.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        QuestionArray[QuestionNum - 1].QuestionC = lines[i + 1];

                    }

                    if (currentLine.Contains("d.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        QuestionArray[QuestionNum - 1].QuestionD = lines[i + 1];

                    }

                    if (currentLine.Contains("e.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        QuestionArray[QuestionNum - 1].QuestionE = lines[i + 1];
                    }

                    if (currentLine.Contains("The correct answer is:"))
                    {
                        QuestionArray[QuestionNum - 1].Answer = currentLine.Substring(currentLine.IndexOf(":") + 2);
                    }

                    if (currentLine.Contains("The correct answers are:"))
                    {
                        //TODO Special Case multiple answers

                        QuestionArray[QuestionNum - 1].Answer = currentLine.Substring(currentLine.IndexOf(":") + 2);
                    }

                    if (currentLine.Contains("The correct answer is \'"))
                    {
                        int Start = currentLine.IndexOf("\'") + 1;
                        int End = currentLine.Length - currentLine.IndexOf("\'") - 3;
                        QuestionArray[QuestionNum - 1].Answer = currentLine.Substring(Start, End);
                    }
                }
                else if (QuestionArray.Count == 1)
                {
                    var currentQuestion = QuestionArray[0];

                    if (currentLine.Contains("a.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        currentQuestion.QuestionA = lines[i + 1];
                    }

                    if (currentLine.Contains("b.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        currentQuestion.QuestionB = lines[i + 1];

                    }

                    if (currentLine.Contains("c.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        currentQuestion.QuestionC = lines[i + 1];

                    }

                    if (currentLine.Contains("d.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        currentQuestion.QuestionD = lines[i + 1];

                    }

                    if (currentLine.Contains("e.") && i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i - 1]))
                    {
                        currentQuestion.QuestionE = lines[i + 1];
                    }

                    if (currentLine.Contains("The correct answer is:"))
                    {
                        currentQuestion.Answer = currentLine.Substring(currentLine.IndexOf(":") + 2);
                    }

                    if (currentLine.Contains("The correct answers are:"))
                    {
                        //TODO Special Case multiple answers
                        //Console.WriteLine("Made it to multiple answers");
                        currentQuestion.Answer = currentLine.Substring(currentLine.IndexOf(":") + 2);
                    }

                    if (currentLine.Contains("The correct answer is \'"))
                    {
                        int Start = currentLine.IndexOf("\'") + 1;
                        int End = currentLine.Length - currentLine.IndexOf("\'") - 3;
                        currentQuestion.Answer = currentLine.Substring(Start, End);
                    }
                }
            }

            //No Duplicates:

            var noDupes = QuestionArray.GroupBy(x => x.QuestionText).Select(x => x.First()).ToList();

            CallFlashcardCreator(path, quiz, noDupes);
        }

        public static void DefineParse(string path, string quiz)
        {

            string[] lines = System.IO.File.ReadAllLines(quiz);

            int questionsFound = 0;

            List<int> questionIndex = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {

                if (lines[i].Contains("Question text"))
                {
                    //System.Console.WriteLine("found question at index: " + i);
                    questionIndex.Add(i);
                    questionsFound++;
                }
            }

            //parse 1 question


            var questions = new List<Question>();

            var solver = new GoogleSolver();
            for (int i = 0; i < lines.Length; i++)
            {
                //Looping through all lines

                if (lines[i].Contains("Question text"))
                {
                    var newQuestion = new Question(lines[i + 1], 0, "");
                    newQuestion.Answer = solver.EnumerateGoogleAnswers(newQuestion.QuestionText)
                        .Select(a => a.Description)
                        .FirstOrDefault()
                        ?? "No results.";
                    questions.Add(newQuestion);
                }

            }



            //CallFlashcardCreator(path, quiz, noDupes);

        }

    }
}
