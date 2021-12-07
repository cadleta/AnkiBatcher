﻿using System;
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
            Quizlet,
        }

        public enum flashcardStyleEnum
        {
            AllMultipleChoice,
            MCTFCloze,
            AdvancedCloze,
            Basic,
            Dynamic,
        }

        public static parseStyleEnum parseStyle = parseStyleEnum.SelfQuiz;
        public static flashcardStyleEnum flashcardStyle = flashcardStyleEnum.Dynamic;
        public static string outputFile = "";
        public static string inputFile = "";

        static void Main(string[] args)
        {
            //Select File: 

            string tempQuiz;
            Console.WriteLine("Enter Text File Path For Input: ");
            tempQuiz = @"" + Console.ReadLine();
            inputFile = tempQuiz.Trim(new Char[] { '"' });
            while (!File.Exists(inputFile))
            {
                Console.WriteLine("Failed, Not a real file.");
                Console.WriteLine("Please Try Again.");
                Console.WriteLine("Enter Text File Path For Input:");
                tempQuiz = @"" + Console.ReadLine();
                inputFile = tempQuiz.Trim(new Char[] { '"' });
            }
            //FileInfo file = new FileInfo(inputFile);
            Console.WriteLine(inputFile);

            string tempPath;
            Console.WriteLine("Enter Output File Name: ");
            tempPath = Console.ReadLine();

            string userFileName = inputFile.Substring(0,inputFile.LastIndexOf(@"\")+1);

            outputFile = userFileName + tempPath + ".txt";
            //FileInfo file = new FileInfo(inputFile);
            Console.WriteLine(outputFile);

            if (!File.Exists(outputFile))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(outputFile))
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

                        // Assign flashcardStyle a value here
                        switch (noteType)
                        {
                            case 0:
                                flashcardStyle = flashcardStyleEnum.AllMultipleChoice;
                                Console.WriteLine(noteType + ": All Multiple Choice");
                                break;
                            case 1:
                                flashcardStyle = flashcardStyleEnum.MCTFCloze;
                                Console.WriteLine(noteType + ": Multiple Choice & True/False Cloze");
                                break;
                            case 2:
                                flashcardStyle = flashcardStyleEnum.AdvancedCloze;
                                Console.WriteLine(noteType + ": Advanced Cloze");
                                break;
                            case 3:
                                flashcardStyle = flashcardStyleEnum.Basic;
                                Console.WriteLine(noteType + ": Basic");
                                break;
                            case 4:
                                flashcardStyle = flashcardStyleEnum.Dynamic;
                                Console.WriteLine(noteType + ": Dynamic");
                                break;
                            default:
                                Console.WriteLine("Error: noteType is not acceptable");
                                break;
                        }

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
                Console.WriteLine("2 - Quizlet \n");


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
                                parseStyle = parseStyleEnum.Quizlet;
                                Console.WriteLine(parseMethod + ": Quizlet Parser");
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
                    TextParsers.SelfQuizParse();
                    break;
                case parseStyleEnum.Define:
                    TextParsers.DefineParse();
                    break;
                case parseStyleEnum.Quizlet:
                    TextParsers.QuizletParse();
                    break;
                default:
                    Console.WriteLine("Error: parseStyle is not acceptable");
                    break;
            }

            #endregion

        }

    }

}
