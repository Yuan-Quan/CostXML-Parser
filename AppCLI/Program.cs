using System;
using CommandLine;

namespace CostXMLParser.CLI
{

    class Program
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Input file to read.")]
            public string? InputFile { get; set; }
        }

        static void Main(string[] args)
        {
            var parsedArgs = Parser.Default.ParseArguments<Options>(args);

            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(parsedArgs.Value.InputFile);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

    }
}