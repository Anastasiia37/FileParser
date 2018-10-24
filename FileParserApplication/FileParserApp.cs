// <copyright file="FileParserApp.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Text;
using FileParserLib;

namespace FileParserUI
{
    /// <summary>
    /// Class of application for file parsing
    /// </summary>
    class FileParserApp
    {
        /// <summary>
        /// The null arguments count in command line
        /// </summary>
        private const int NULL_ARGUMENTS_COUNT = 0;

        /// <summary>
        /// The not enough arguments count in command line
        /// </summary>
        private const int NOT_ENOUGH_ARGUMENTS_COUNT = 1;

        /// <summary>
        /// The minimum arguments count  in command line
        /// </summary>
        private const int MIN_ARGUMENTS_COUNT = 2;

        /// <summary>
        /// The maximum arguments count  in command line
        /// </summary>
        private const int MAX_ARGUMENTS_COUNT = 3;

        /// <summary>
        /// The starting point of application
        /// </summary>
        /// <param name="args">The arguments from command line</param>
        /// <returns>Return Code</returns>

        public int Run(string[] args)
        {
            Logger.InitLogger();
            try
            {
                FileParser fileParser;
                switch (args.Length)
                {
                    case NULL_ARGUMENTS_COUNT:
                        this.ShowAbout();
                        Console.ReadKey();
                        break;
                    case NOT_ENOUGH_ARGUMENTS_COUNT:
                        throw new ArgumentException("You don't have enough arguments in command line!");
                    case MIN_ARGUMENTS_COUNT:
                        fileParser = StringCounter.Initialize(args[0], args[1]);
                        int stringsCount = fileParser.Parse();
                        Console.WriteLine($"There are {stringsCount} strings in file {args[0]}");
                        break;
                    case MAX_ARGUMENTS_COUNT:
                        fileParser = StringReplacer.Initialize(args[0], args[1], args[2]);
                        int replacedStringsCount = fileParser.Parse();
                        Console.WriteLine($"There were {replacedStringsCount} strings replaced in file {args[0]}");
                        break;
                    default:
                        if (args.Length > MAX_ARGUMENTS_COUNT)
                        {
                            throw new ArgumentException("You have too many arguments in command line!");
                        }

                        break;
                }
            }
            catch (ArgumentException exception)
            {
                HandleExceptions(exception, args);
                return (int)ReturnCode.Error;
            }
            catch (FileNotFoundException exception)
            {
                HandleExceptions(exception, args);
                return (int)ReturnCode.Error;
            }

            Logger.Log.Info("The program was ended with return code: Success. "
                + "The command line arguments: " + this.GetArgumentsAsString(args));
            return (int)ReturnCode.Success;
        }

        private void HandleExceptions(Exception exception, string[] args)
        {
            string logMessage = exception + Environment.NewLine +
                    "The command line arguments: " + this.GetArgumentsAsString(args) + Environment.NewLine;
            Logger.Log.Error(logMessage);
            Console.WriteLine(exception.Message);
            this.ShowInstructions();
        }

        /// <summary>
        /// Allows to convert an array of strings to a single string
        /// </summary>
        /// <param name="args">Array of strings</param>
        /// <returns>A string that consists of an array of input strings</returns>
        private string GetArgumentsAsString(string[] args)
        {
            StringBuilder stringArgs = new StringBuilder();
            foreach (string argument in args)
            {
                stringArgs.Append(argument).Append(" ");
            }

            return stringArgs.ToString();
        }

        /// <summary>
        /// Shows the information about the application
        /// </summary>
        private void ShowAbout()
        {
            Console.WriteLine(FileParserApplication.Properties.Resources.ReadMe);                
        }

        /// <summary>
        /// Shows the instructions how to use the application
        /// </summary>
        private void ShowInstructions()
        {
            Console.WriteLine(Environment.NewLine + "Input parameters:" + Environment.NewLine
                + "1. <file path> <line to count>" + Environment.NewLine
                + "2. <file path> <search string> <replacement string>");
        }
    }
}