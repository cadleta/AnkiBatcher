using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AnkiBatcher
{
    public class Program
    {

        public enum parseStyleEnum
        {
            SelfQuiz,
            Define,
            Dynamic,
        }

        public enum flashcardStyleEnum
        {
            AllMultipleChoice,
            MCTFCloze,
            AdvancedCloze,
            Basic,
            Dynamic,
        }

        // TODO: make path and quiz file paths public
        public static parseStyleEnum parseStyle = parseStyleEnum.Dynamic;
        public static flashcardStyleEnum flashcardStyle = flashcardStyleEnum.Dynamic;

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

                        // TODO: assign flashcardStyle a value here
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
            #region GetValidParseMethod

            int parseMethod = -1;
            bool modeValid = false;

            // Repeat asking what note type until an accepted answer is submitted
            while (!modeValid)
            {
                Console.WriteLine("What Mode Would You Like?");
                Console.WriteLine("Enter the number for the corresponding mode: \n");
                Console.WriteLine("0 - Self Quiz Decoder");
                Console.WriteLine("1 - Definition Researcher");
                Console.WriteLine("2 - Dynamic \n");


                bool parsed = int.TryParse(Console.ReadLine(), out parseMethod);

                // If entered text is an accepted number:
                if (parsed)
                {

                    // If accepted number is in option range:
                    if (parseMethod <= 3 && parseMethod >= 0)
                    {
                        // Exit loop and print noteType
                        modeValid = true;

                        switch (parseMethod)
                        {
                            case 0:
                                parseStyle = parseStyleEnum.SelfQuiz;
                                Console.WriteLine(parseMethod + ": Self Quiz Parser");
                                break;
                            case 1:
                                parseStyle = parseStyleEnum.Define;
                                Console.WriteLine(parseMethod + ": Define Parser");
                                break;
                            case 2:
                                parseStyle = parseStyleEnum.Dynamic;
                                Console.WriteLine(parseMethod + ": Dynamic Parser");
                                break;
                            default:
                                Console.WriteLine("Error: parseMethod is not acceptable");
                                break;
                        }

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

            // Start by parsing mode
            #region SetParser

            switch (parseStyle){
                case parseStyleEnum.SelfQuiz:
                    TextParsers.SelfQuizParse(path,quiz);
                    break;
                case parseStyleEnum.Define:
                    break;
                case parseStyleEnum.Dynamic:
                    break;
                default:
                    Console.WriteLine("Error: parseStyle is not acceptable");
                    break;
            }

            #endregion

        }

    }

}
