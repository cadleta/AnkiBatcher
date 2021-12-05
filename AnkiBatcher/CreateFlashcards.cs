using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkiBatcher
{
    class CreateFlashcards
    {

        public static void AllMultipleChoice(List<Question> questions)
        {

            using (StreamWriter sw = File.AppendText(Program.outputFile))
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
                        }
                        else if (string.IsNullOrWhiteSpace(item.QuestionD))
                        {
                            totalChoices = 3;
                        }
                        else if (string.IsNullOrWhiteSpace(item.QuestionE))
                        {
                            totalChoices = 4;
                        }
                        else
                        {
                            totalChoices = 5;
                        }


                        bool[] correct = new bool[totalChoices];
                        foreach (bool choice in correct)
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
                                answers[1] = item.Answer.Substring(item.Answer.IndexOf(",") + 2);
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
                            if (oneAnswer.Equals(item.QuestionA))
                            {
                                correct[0] = true;
                            }
                            else if (oneAnswer.Equals(item.QuestionB))
                            {
                                correct[1] = true;
                            }
                            else if (oneAnswer.Equals(item.QuestionC))
                            {
                                correct[2] = true;
                            }
                            else if (oneAnswer.Equals(item.QuestionD))
                            {
                                correct[3] = true;
                            }
                            else if (oneAnswer.Equals(item.QuestionE))
                            {
                                correct[4] = true;
                            }
                        }

                        foreach (bool choice in correct)
                        {
                            if (choice)
                            {
                                sw.Write("1");
                            }
                            else
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

            OpenWithDefaultProgram(Program.outputFile);
        }

        public static void MCTFCloze(List<Question> questions)
        {
            string path = Program.outputFile;
            string quiz = Program.inputFile;

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
            string userFileName2 = path.Substring(0, path.LastIndexOf(@".") - 15);

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
        public static void AdvancedCloze(List<Question> questions)
        {

        }

        public static void Basic(List<Question> questions)
        {
            using (StreamWriter sw = File.AppendText(Program.outputFile))
            {
                foreach (Question item in questions)
                {
                    sw.WriteLine(item.QuestionText + "|" + item.Answer);
                }
            }

            //Open Text File

            OpenWithDefaultProgram(Program.outputFile);
        }

        public static void Dynamic(List<Question> questions)
        {

        }

        public static void OpenWithDefaultProgram(string path)
        {
            using Process fileopener = new Process();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }

    }
}
