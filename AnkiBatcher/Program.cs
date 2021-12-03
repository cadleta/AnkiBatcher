using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AnkiBatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Select File: 

            string tempQuiz;
            Console.WriteLine("Enter Text File Path For Input: ");
            tempQuiz = @"" + Console.ReadLine();
            string quiz = tempQuiz.Trim(new Char[] { '"' });
            while (!File.Exists(quiz))
            {
                Console.WriteLine("Failed, Not a real file.");
                Console.WriteLine("Please Try Again.");
                Console.WriteLine("Enter Text File Path For Input:");
                tempQuiz = @"" + Console.ReadLine();
                quiz = tempQuiz.Trim(new Char[] { '"' });
            }
            //FileInfo file = new FileInfo(quiz);
            Console.WriteLine(quiz);

            string tempPath;
            Console.WriteLine("Enter Output File Name: ");
            tempPath = Console.ReadLine();

            string userFileName = quiz.Substring(0,quiz.LastIndexOf(@"\")+1);

            string path = userFileName + tempPath + ".txt";
            //FileInfo file = new FileInfo(quiz);
            Console.WriteLine(path);

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("");
                }
            }


            // Ask the user for a valid note type
            #region GetValidNoteType

            int noteType = -1;
            bool noteTypeValid = false;

            // Repeat asking what note type until an accepted answer is submitted
            while (!noteTypeValid)
            {
                Console.WriteLine("What Note Type Would You Like?");
                Console.WriteLine("Enter the number for the corresponding mode:");
                Console.WriteLine("0 - All Multiple Choice");
                Console.WriteLine("1 - Multiple Choice & True/False Cloze");
                Console.WriteLine("2 - Advanced Cloze");
                Console.WriteLine("3 - Basic");
                Console.WriteLine("4 - Dynamic \n");
                
                bool parsed = int.TryParse(Console.ReadLine(), out noteType);

                // If entered text is an accepted number:
                if (parsed)
                {

                    // If accepted number is in option range:
                    if (noteType <= 4 && noteType >= 0)
                    {
                        // Exit loop and print noteType
                        noteTypeValid = true;
                        Console.WriteLine(noteType);
                    }
                    else
                    {
                        // accepted number is not an option
                        noteTypeValid = false;
                        Console.WriteLine("\nError: Accepted number is not an option\n");
                    }
                } 
                else
                {
                    // Number was not parsed
                    noteTypeValid = false;
                    Console.WriteLine("\nError: Unable to parse int\n");
                } 
            }
            #endregion

            // Ask the user for a valid mode type
            #region GetValidModeType

            int mode = -1;
            bool modeValid = false;

            // Repeat asking what note type until an accepted answer is submitted
            while (!modeValid)
            {
                Console.WriteLine("What Mode Would You Like?");
                Console.WriteLine("Enter the number for the corresponding mode: \n");
                Console.WriteLine("0 - Self Quiz Decoder");
                Console.WriteLine("1 - Definition Researcher");
                Console.WriteLine("2 - Book Quotes");
                Console.WriteLine("3 - Based On Note Type \n");


                bool parsed = int.TryParse(Console.ReadLine(), out mode);

                // If entered text is an accepted number:
                if (parsed)
                {

                    // If accepted number is in option range:
                    if (mode <= 4 && mode >= 0)
                    {
                        // Exit loop and print noteType
                        modeValid = true;
                        Console.WriteLine(mode);
                    }
                    else
                    {
                        // accepted number is not an option
                        modeValid = false;
                        Console.WriteLine("\nError: Accepted number is not an option\n");
                    }
                }
                else
                {
                    // Number was not parsed
                    modeValid = false;
                    Console.WriteLine("\nError: Unable to parse int\n");
                }
            }

            #endregion

        }

        public static void SelfQuizParse(string path, string quiz, int noteType)
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
                } else if (currentLine.Contains("Select one or more:"))
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
                } else if (QuestionArray.Count == 1)
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


            switch (noteType)
            {
                case 0:
                    AllMultipleChoice(path, quiz, noDupes);
                    break;
                case 1:
                    MCTFCloze(path, quiz, noDupes);
                    break;
                case 2:
                    AdvancedCloze(path, quiz, noDupes);
                    break;
                case 3:
                    Basic(path, quiz, noDupes);
                    break;
                default:
                    AllMultipleChoice(path, quiz, noDupes);
                    break;
            }


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

            //Basic(path, quiz,);

        }

        public static void AllMultipleChoice(string path, string quiz, List<Question> questions)
        {

            using (StreamWriter sw = File.AppendText(path))
            {
                foreach (Question item in questions)
                {
                    if (item.QuestionType == 1)
                    {
                        sw.Write(item.QuestionText + "| | 2 |" + item.QuestionA + "|" + item.QuestionB + "|" + item.QuestionC + "|" + item.QuestionD + "|" + item.QuestionE + "|");
                        if (item.Answer.Equals(item.QuestionA))
                        {
                            if (item.QuestionE == null)
                            {
                                sw.Write("1 0 0 0");
                            }
                            else
                            {
                                sw.Write("1 0 0 0 0");
                            }
                        }
                        else if (item.Answer.Equals(item.QuestionB))
                        {
                            if (item.QuestionE == null)
                            {
                                sw.Write("0 1 0 0");
                            }
                            else
                            {
                                sw.Write("0 1 0 0 0");
                            }
                        }
                        else if (item.Answer.Equals(item.QuestionC))
                        {
                            if (item.QuestionE == null)
                            {
                                sw.Write("0 0 1 0");
                            }
                            else
                            {
                                sw.Write("0 0 1 0 0");
                            }
                        }
                        else if (item.Answer.Equals(item.QuestionD))
                        {
                            if (item.QuestionE == null)
                            {
                                sw.Write("0 0 0 1");
                            }
                            else
                            {
                                sw.Write("0 0 0 1 0");
                            }
                        }
                        else if (item.Answer.Equals(item.QuestionE))
                        {
                            sw.Write("0 0 0 0 1");
                        }
                        else
                        {
                            sw.Write("0 0 0 0");
                        }
                        sw.WriteLine("");

                    }
                    else if (item.QuestionType == 0)
                    {
                        sw.Write(item.QuestionText + "| | 2 |" + item.QuestionA + "|" + item.QuestionB + "| | | |");
                        if (item.Answer.Equals(item.QuestionA))
                        {
                            sw.Write("1 0");
                        }
                        else if (item.Answer.Equals(item.QuestionB))
                        {
                            sw.Write("0 1");
                        }
                        sw.WriteLine("");
                    }
                    else if (item.QuestionType == 2)
                    {
                        sw.Write(item.QuestionText + "| | 1 |" + item.QuestionA + "|" + item.QuestionB + "|" + item.QuestionC + "|" + item.QuestionD + "|" + item.QuestionE + "|");
                        char ch = ',';

                        int ans = item.Answer.Count(f => (f == ch));
                        string[] answers = new string[ans + 1];

                        int totalChoices = 4;

                        if (string.IsNullOrWhiteSpace(item.QuestionC))
                        {
                            totalChoices = 2;
                        } else if (string.IsNullOrWhiteSpace(item.QuestionD))
                        {
                            totalChoices = 3;
                        } else if (string.IsNullOrWhiteSpace(item.QuestionE))
                        {
                            totalChoices = 4;
                        } else
                        {
                            totalChoices = 5;
                        }


                        bool[] correct = new bool[totalChoices];
                        foreach(bool choice in correct)
                        {
                            choice.Equals(false);
                            //Console.WriteLine(choice);
                        }

                        switch (ans)
                        {
                            //only one answer
                            case 0:
                                answers[0] = item.Answer;
                                break;

                            //2 answers
                            case 1:
                                answers[0] = item.Answer.Substring(0, item.Answer.IndexOf(","));
                                answers[1] = item.Answer.Substring(item.Answer.IndexOf(",")+2);
                                Console.WriteLine(answers[0] + " " + answers[1]);
                                break;

                            //3 answers
                            case 2:
                                answers[0] = item.Answer.Substring(0, item.Answer.IndexOf(","));
                                answers[1] = item.Answer.Substring(item.Answer.IndexOf(",") + 2, item.Answer.LastIndexOf(","));
                                answers[2] = item.Answer.Substring(item.Answer.LastIndexOf(",") + 2);
                                break;

                            //4 answers
                            case 3:
                                
                                break;

                            //5 answers
                            case 4:

                                break;
                        }

                        foreach (string oneAnswer in answers)
                        {
                            if (oneAnswer.Equals(item.QuestionA)){
                                correct[0] = true;
                            } else if (oneAnswer.Equals(item.QuestionB))
                            {
                                correct[1] = true;
                            } else if (oneAnswer.Equals(item.QuestionC))
                            {
                                correct[2] = true;
                            } else if (oneAnswer.Equals(item.QuestionD))
                            {
                                correct[3] = true;
                            } else if (oneAnswer.Equals(item.QuestionE))
                            {
                                correct[4] = true;
                            }
                        }

                        foreach(bool choice in correct)
                        {
                            if (choice)
                            {
                                sw.Write("1");
                            } else
                            {
                                sw.Write("0");
                            }
                            sw.Write(" ");
                        }

                        sw.WriteLine("");
                    }
                }
            }

            //Open Text File

            OpenWithDefaultProgram(path);
        }

        public static void OpenWithDefaultProgram(string path)
        {
            using Process fileopener = new Process();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }

        public static void MCTFCloze(string path, string quiz, List<Question> questions)
        {
            //Creating Second File:
            string userFileName = path.Substring(0, path.LastIndexOf(@"."));

            path = userFileName + " Multiple Choice.txt";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("");
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                foreach (Question item in questions)
                {
                    if (item.QuestionType == 1)
                    {
                        sw.Write(item.QuestionText + "| | 2 |" + item.QuestionA + "|" + item.QuestionB + "|" + item.QuestionC + "|" + item.QuestionD + "| |");
                        if (item.Answer.Equals(item.QuestionA))
                        {
                            sw.Write("1 0 0 0");
                        }
                        else if (item.Answer.Equals(item.QuestionB))
                        {
                            sw.Write("0 1 0 0");
                        }
                        else if (item.Answer.Equals(item.QuestionC))
                        {
                            sw.Write("0 0 1 0");
                        }
                        else if (item.Answer.Equals(item.QuestionD))
                        {
                            sw.Write("0 0 0 1");
                        }
                        sw.WriteLine("");
                    }
                }
            }

            //Creating Second File:
            string userFileName2 = path.Substring(0, path.LastIndexOf(@".")-15);

            string path2 = userFileName2 + " True False Cloze.txt";

            if (!File.Exists(path2))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path2))
                {
                    sw.WriteLine("");
                }
            }

            //TODO: Write the Cloze True False Questions To Text File
            //Can Write to file
            //Only getting 16 questions out of 20 questions from document
            //Not getting questions of right type?



            using (StreamWriter sw = File.AppendText(path2))
            {
                foreach (Question item in questions)
                {
                    if (item.QuestionType == 0)
                    {
                        sw.WriteLine("<b>True/False:</b> " + item.QuestionText + " {{c1::" + item.Answer + "}}");
                    }
                }
            }

            //Open Text File

            OpenWithDefaultProgram(path);
            OpenWithDefaultProgram(path2);
        }
        public static void AdvancedCloze(string path, string quiz, List<Question> questions)
        {

        }

        public static void Basic(string path, string quiz, List<Question> questions)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                foreach (Question item in questions)
                {
                    sw.WriteLine(item.QuestionText + "|" + item.Answer);
                }
            }

            //Open Text File

            OpenWithDefaultProgram(path);
        }

    }

}
