using System;
using System.Xml.Linq;
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

            var deserializer = new Deserializer(parsedArgs.Value.InputFile);
        }

    }
}